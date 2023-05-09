using AutoMapper;
using Microsoft.Extensions.Logging;
using OnlineStore.BusinessLogic.Contracts.Dtos.Product;
using OnlineStore.BusinessLogic.Contracts.Dtos.ProductVariant.Base;
using OnlineStore.BusinessLogic.Contracts.Interfaces;
using OnlineStore.BusinessLogic.Exceptions;
using OnlineStore.Data.Contracts.Entities.Products;
using OnlineStore.Data.Contracts.Interfaces;
using OnlineStore.Data.Contracts.Models;

namespace OnlineStore.BusinessLogic.Services
{
    public class ProductService : IProductService
    {
        private readonly ILogger<ProductService> _logger;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IMapper _mapper;
        public ProductService(
            ILogger<ProductService> logger,
            IUnitOfWorkFactory unitOfWorkFactory,
            IMapper mapper)
        {
            _logger = logger;
            _unitOfWorkFactory = unitOfWorkFactory;
            _mapper = mapper;
        }

        public async Task<PagedList<ProductDto>> GetAllAsync(ProductGetAllRequestDto productGetAllRequestDto)   
        {
            _logger.LogInformation("GetAllAsync called");

            using var uow = _unitOfWorkFactory.Create();
            var productParameters = _mapper.Map<ProductParameters>(productGetAllRequestDto);
            var pagedProducts = await uow.ProductRepository.GetProductsAsync(productParameters);

            _logger.LogInformation("Retrieved {Count} products from database", pagedProducts.Count);

            var productDtos = pagedProducts.Select(p => _mapper.Map<ProductDto>(p)).ToList();
            var pagedProductDtos = new PagedList<ProductDto>(productDtos, pagedProducts.TotalCount, pagedProducts.CurrentPage, pagedProducts.PageSize);

            return pagedProductDtos;
        }
        public async Task<ProductDto> GetByIdAsync(int productId)
        {
            _logger.LogInformation("GetByIdAsync called with productId: {ProductId}", productId);

            using var uow = _unitOfWorkFactory.Create();
            var product = await uow.ProductRepository.GetProductWithDetailsAsync(productId);

            if (product == null)
            {
                _logger.LogWarning("Product with ID {ProductId} not found", productId);
                throw new NotFoundException();
            }

            var productDto = _mapper.Map<ProductDto>(product);

            _logger.LogInformation("GetByIdAsync returned product with ID {ProductId}", productId);

            return productDto;
        }

        public async Task<ProductDto> CreateAsync(CreateProductDto createProductDto)
        {
            _logger.LogInformation("CreateAsync called with createProductDto: {@CreateProductDto}", createProductDto);

            using var uow = _unitOfWorkFactory.Create();
            var product = _mapper.Map<Product>(createProductDto);

            if (createProductDto.Options.Count == 0 && createProductDto.Variants.Count == 0)
            {
                HandleAsIsProductCase(product, createProductDto.SKU, createProductDto.RegularPrice, createProductDto.SalePrice, createProductDto.Quantity);
            }
            else
            {          
                var zippedVariants = product.Variants.Zip(createProductDto.Variants);
                var updatedVariants = GetUpdatedVariants(zippedVariants, product.Options.ToList());

                product.Variants.Clear();

                foreach (var updatedVariant in updatedVariants)
                {
                    product.Variants.Add(updatedVariant);
                }
            }

            await uow.ProductRepository.AddAsync(product);
            await uow.SaveChangesAsync();

            var productDto = _mapper.Map<ProductDto>(product);

            _logger.LogInformation("CreateAsync created product with ID {ProductId}", productDto.Id);

            return productDto;        
        }

        /// <summary>
        /// Manual object graph update.
        /// </summary>
        /// <param name="updateProductDto"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task<ProductDto> UpdateAsync(UpdateProductDto updateProductDto)
        {
            _logger.LogInformation("UpdateAsync called with updateProductDto: {@UpdateProductDto}", updateProductDto);

            using var uow = _unitOfWorkFactory.Create();

            if (await uow.ProductRepository.GetByIdAsync(updateProductDto.Id) == null)
            {
                _logger.LogWarning("Product with ID {ProductId} not found", updateProductDto.Id);
                throw new NotFoundException();
            }

            var product = _mapper.Map<Product>(updateProductDto);

            foreach (var option in product.Options)
            {
                foreach (var optionValue in option.OptionValues)
                {
                    var existingOptionValue = await uow.OptionValueRepository
                        .GetBySpecAsync<(string, int)>(ov => ov.Value == optionValue.Value
                            && ov.OptionId == optionValue.OptionId
                            && optionValue.OptionId != default);

                    if (existingOptionValue == null)
                    {
                        await uow.OptionValueRepository.AddAsync(optionValue);
                    }
                    else
                    {
                        optionValue.OptionValueId = existingOptionValue.OptionValueId;
                    }
                }
            }
            await uow.SaveChangesAsync();

            if (updateProductDto.Options.Count == 0 && updateProductDto.Variants.Count == 0)
            {
                HandleAsIsProductCase(product, updateProductDto.SKU, updateProductDto.RegularPrice, updateProductDto.SalePrice, updateProductDto.Quantity);
            }
            else
            {
                var zippedVariants = product.Variants.Zip(updateProductDto.Variants);
                var updatedVariants = GetUpdatedVariants(zippedVariants, product.Options);

                product.Variants.Clear();

                foreach (var updatedVariant in updatedVariants)
                {
                    product.Variants.Add(updatedVariant);
                }
            }

            var existingProduct = await uow.ProductRepository.GetProductWithDetailsAsync(product.ProductId, false);

            uow.ProductRepository.Update(existingProduct, product);

            UpdateOptions(existingProduct.Options, product.Options, uow);
            UpdateVariants(existingProduct.Variants, product.Variants, uow);

            await uow.SaveChangesAsync();

            var productDto = _mapper.Map<ProductDto>(existingProduct);

            _logger.LogInformation("Product with ID {ProductId} updated in UpdateAsync", productDto.Id);

            return productDto;
        }

        public async Task DeleteAsync(int productId)
        {
            _logger.LogInformation("DeleteAsync called with productId: {@productId}", productId);

            using var uow = _unitOfWorkFactory.Create();
            var productToDelete = await uow.ProductRepository.GetByIdAsync(productId);

            if (productToDelete == null)
            {
                _logger.LogWarning("Product with ID {ProductId} not found", productId);
                throw new NotFoundException();
            }

            uow.ProductRepository.Delete(productToDelete);
            await uow.SaveChangesAsync();

            _logger.LogInformation("Product with ID {ProductId} was deleted in DeleteAsync", productId);
        }

        private static void UpdateOptions(
            ICollection<Option> existingOptions, 
            ICollection<Option> updatedOptions,
            IUnitOfWork uow)
        {
            foreach (var existingOption in existingOptions.ToList())
            {
                if (!updatedOptions.Any(o => o.OptionId == existingOption.OptionId))
                {
                    uow.OptionRepository.Delete(existingOption);
                }
            }

            foreach (var updatedOption in updatedOptions)
            {
                var existingOption = existingOptions
                    .Where(o => o.OptionId == updatedOption.OptionId && o.OptionId != default)
                    .SingleOrDefault();

                if (existingOption != null)
                {
                    uow.OptionRepository.Update(existingOption, updatedOption);
                    UpdateOptionValues(existingOption.OptionValues, updatedOption.OptionValues, uow);
                }
                else
                {
                    existingOptions.Add(updatedOption);
                }
            }
        }

        private static void UpdateOptionValues(
            ICollection<OptionValue> existingOptionValues, 
            ICollection<OptionValue> updatedOptionValues,
            IUnitOfWork uow)
        {
            foreach (var existingOptionValue in existingOptionValues.ToList())
            {
                if (!updatedOptionValues.Any(ov => ov.OptionValueId == existingOptionValue.OptionValueId))
                {
                    uow.OptionValueRepository.Delete(existingOptionValue);
                }
            }

            foreach (var updatedOptionValue in updatedOptionValues)
            {
                var existingOptionValue = existingOptionValues
                    .Where(ov => ov.OptionValueId == updatedOptionValue.OptionValueId && updatedOptionValue.OptionValueId != default)
                    .SingleOrDefault();

                if (existingOptionValue == null)
                {
                    existingOptionValues.Add(updatedOptionValue);
                }
            }
        }

        private static void UpdateVariants(
            ICollection<Variant> existingVariants,
            ICollection<Variant> updatedVariants,
            IUnitOfWork uow)
        {
            foreach (var existingVariant in existingVariants.ToList())
            {
                if (!updatedVariants.Any(v => v.VariantId == existingVariant.VariantId))
                {
                    // Delete all related VariantOptionValue objects (manual cascading deletes)
                    uow.VariantOptionValueRepository.DeleteRange(existingVariant.VariantOptionValues);

                    uow.VariantRepository.Delete(existingVariant);
                }
            }

            foreach (var updatedVariant in updatedVariants)
            {
                var existingVariant = existingVariants
                    .Where(v => v.VariantId == updatedVariant.VariantId && v.VariantId != default)
                    .SingleOrDefault();

                if (existingVariant != null)
                {
                    uow.VariantRepository.Update(existingVariant, updatedVariant);
                    UpdateVariantOptionValues(existingVariant.VariantOptionValues, updatedVariant.VariantOptionValues, uow);
                }
                else
                {
                    existingVariants.Add(updatedVariant);
                }
            }
        }

        private static void UpdateVariantOptionValues(
            ICollection<VariantOptionValue> existingVariantOptionValues,
            ICollection<VariantOptionValue> updatedVariantOptionValues,
            IUnitOfWork uow)
        {
            foreach (var existingVariantOptionValue in existingVariantOptionValues.ToList())
            {
                if (!updatedVariantOptionValues.Any(vov => vov.OptionValueId == existingVariantOptionValue.OptionValueId))
                {
                    uow.VariantOptionValueRepository.Delete(existingVariantOptionValue);
                }
            }

            foreach (var updatedVariantOptionValue in updatedVariantOptionValues)
            {
                var existingVariantOptionValue = existingVariantOptionValues
                    .Where(vov => vov.OptionValueId == updatedVariantOptionValue.OptionValueId && vov.OptionValueId != default)
                    .SingleOrDefault();

                if (existingVariantOptionValue == null)
                {
                    existingVariantOptionValues.Add(updatedVariantOptionValue);
                }
            }
        }


        /// <summary>
        /// Adds a default product variant as there is always the pseudo option of the product "as-is".
        /// </summary>
        /// <param name="product"></param>
        /// <param name="sku"></param>
        /// <param name="regularPrice"></param>
        /// <param name="salePrice"></param>
        /// <param name="quantity"></param>
        private static void HandleAsIsProductCase(
            Product product, 
            string sku, 
            decimal regularPrice, 
            decimal? salePrice, 
            int? quantity)
        {     
            var defaultVariant = new Variant()
            {
                Product = product.ProductId > 0 ? null : product,
                ProductId = product.ProductId,
                SKU = sku,
                RegularPrice = regularPrice,
                SalePrice = salePrice,
                Quantity = quantity
            };
            product.Variants.Add(defaultVariant);
        }

        /// <summary>
        /// Maps a set of key-value pairs Choices into the list of VariantOptionValue objects.
        /// </summary>
        /// <typeparam name="TVariantDto"></typeparam>
        /// <param name="zippedVariants"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        private static List<Variant> GetUpdatedVariants<TVariantDto>(
            IEnumerable<(Variant First, TVariantDto Second)> zippedVariants, 
            IEnumerable<Option> options)
            where TVariantDto : ProductVariantBaseDto
        {
            var optionNameToOptionValueMap = options.ToDictionary(o => o.Name, o => o.OptionValues.ToList());

            var updatedVariants = zippedVariants.Select(variantPair =>
            {
                var (existingVariant, updatedVariantDto) = variantPair;

                var variantOptionValues = updatedVariantDto.Choices.Select(choice =>
                {
                    var optionValue = optionNameToOptionValueMap[choice.Key].FirstOrDefault(ov => ov.Value == choice.Value);
                    return new VariantOptionValue
                    {
                        Variant = existingVariant.VariantId > 0 ? null : existingVariant,
                        VariantId = existingVariant.VariantId,
                        OptionValue = optionValue.OptionValueId > 0 ? null : optionValue,
                        OptionValueId = optionValue.OptionValueId
                    };
                }).ToList();

                foreach (var variantOptionValue in variantOptionValues)
                {
                    existingVariant.VariantOptionValues.Add(variantOptionValue);
                }
                return existingVariant;
            }).ToList();

            return updatedVariants;
        }
    }
}
