using Microsoft.EntityFrameworkCore;
using OnlineStore.Data.Contracts.Entities.Products;
using OnlineStore.Data.Contracts.Interfaces;
using OnlineStore.Data.Contracts.Models;
using OnlineStore.Data.Extensions;

namespace OnlineStore.Data.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ISortHelper<Product> _productSortHelper;

        public ProductRepository(
            ApplicationDbContext context, 
            ISortHelper<Product> productSortHelper) 
            : base(context)
        {
            _productSortHelper = productSortHelper;
        }

        public async Task<PagedList<Product>> GetProductsAsync(ProductParameters productParameters)
        {
            var products = GetAll();

            products = products
                        .Include(p => p.Options)
                            .ThenInclude(p => p.OptionValues)
                        .Include(p => p.Variants)
                            .ThenInclude(v => v.VariantOptionValues)
                                .ThenInclude(v => v.OptionValue)
                                    .ThenInclude(o => o.Option);

            SearchByName(ref products, productParameters.Name);

            var sorderProducts = _productSortHelper.ApplySorting(products, productParameters.OrderBy);
            var pagedProducts = await sorderProducts.ToPagedListAsync(productParameters.PageNumber, productParameters.PageSize);
            return pagedProducts;
        }

        public async Task<Product> GetProductWithDetailsAsync<TId>(
            TId id, bool asNoTracking = true, 
            CancellationToken cancellationToken = default)
        {
            IQueryable<Product> queryable = GetAll(asNoTracking);

            return await queryable
                .Where(p => Equals(p.ProductId, id))
                .Include(p => p.Options)
                    .ThenInclude(o => o.OptionValues)
                .Include(p => p.Variants)
                    .ThenInclude(v => v.VariantOptionValues)
                        .ThenInclude(vov => vov.OptionValue)
                            .ThenInclude(ov => ov.Option)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public void Update(Product existingProduct, Product updatedProduct)
        {
            _context.Entry(existingProduct).CurrentValues.SetValues(updatedProduct);
            _context.Entry(existingProduct).Property(p => p.CreatedDate).IsModified = false;
        }

        private static void SearchByName(ref IQueryable<Product> products, string productName)
        {
            if (!products.Any() || string.IsNullOrWhiteSpace(productName))
            {
                return;
            }

            products = products.Where(p => p.Name.ToLower().Contains(productName.Trim().ToLower()));
        }

        /// <summary>
        /// Deprecated (left only for the dummy OnlineStore.Data.UnitTests to work).
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public new Product Update(Product product)
        {
            var existingProduct = _context.Products
                .Where(p => p.ProductId == product.ProductId)
                .Include(p => p.Options)
                    .ThenInclude(o => o.OptionValues)
                .Include(p => p.Variants)
                    .ThenInclude(v => v.VariantOptionValues)
                        .ThenInclude(vov => vov.OptionValue)
                            .ThenInclude(ov => ov.Option)
                .Single();

            _context.Entry(existingProduct).CurrentValues.SetValues(product);
            _context.Entry(existingProduct).Property(p => p.CreatedDate).IsModified = false;

            UpdateOptions(product, existingProduct);
            UpdateVariants(product, existingProduct);

            return existingProduct;
        }
        private void UpdateVariants(Product product, Product existingProduct)
        {
            foreach (var existingVariant in existingProduct.Variants.ToList())
            {
                if (!product.Variants.Any(v => v.VariantId == existingVariant.VariantId))
                {
                    // Delete all related VariantOptionValue objects (manual cascading deletes)
                    _context.RemoveRange(existingVariant.VariantOptionValues);

                    _context.Remove(existingVariant);
                }
            }

            foreach (var variant in product.Variants)
            {
                var existingVariant = existingProduct.Variants
                    .Where(v => v.VariantId == variant.VariantId && v.VariantId != default)
                    .SingleOrDefault();

                if (existingVariant != null)
                {
                    _context.Entry(existingVariant).CurrentValues.SetValues(variant);
                    _context.Entry(existingVariant).Property(v => v.CreatedDate).IsModified = false;

                    UpdateVariantOptionValues(variant, existingVariant);
                }
                else
                {
                    existingProduct.Variants.Add(variant);
                }
            }
        }
        private void UpdateVariantOptionValues(Variant variant, Variant existingVariant)
        {
            foreach (var existingVariantOptionValue in existingVariant.VariantOptionValues.ToList())
            {
                if (!variant.VariantOptionValues.Any(vov => vov.OptionValueId == existingVariantOptionValue.OptionValueId))
                {
                    _context.Remove(existingVariantOptionValue);
                }
            }

            foreach (var variantOptionValue in variant.VariantOptionValues)
            {
                var existingVariantOptionValue = existingVariant.VariantOptionValues
                    .Where(vov => vov.OptionValueId == variantOptionValue.OptionValueId && vov.OptionValueId != default)
                    .SingleOrDefault();

                if (existingVariantOptionValue == null)
                {
                    existingVariant.VariantOptionValues.Add(variantOptionValue);
                }
            }
        }
        private void UpdateOptions(Product product, Product existingProduct)
        {
            foreach (var existingOption in existingProduct.Options.ToList())
            {
                if (!product.Options.Any(o => o.OptionId == existingOption.OptionId))
                {
                    _context.Remove(existingOption);
                }
            }

            foreach (var option in product.Options)
            {
                var existingOption = existingProduct.Options
                    .Where(o => o.OptionId == option.OptionId && o.OptionId != default)
                    .SingleOrDefault();

                if (existingOption != null)
                {
                    _context.Entry(existingOption).CurrentValues.SetValues(option);
                    _context.Entry(existingOption).Property(o => o.CreatedDate).IsModified = false;

                    UpdateOptionValues(option, existingOption);
                }
                else
                {
                    existingProduct.Options.Add(option);
                }
            }
        }
        private void UpdateOptionValues(Option option, Option existingOption)
        {
            foreach (var existingOptionValue in existingOption.OptionValues.ToList())
            {
                if (!option.OptionValues.Any(ov => ov.OptionValueId == existingOptionValue.OptionValueId))
                {
                    _context.Remove(existingOptionValue);
                }
            }

            foreach (var optionValue in option.OptionValues)
            {
                var existingOptionValue = existingOption.OptionValues
                    .Where(ov => ov.OptionValueId == optionValue.OptionValueId && optionValue.OptionValueId != default)
                    .SingleOrDefault();

                if (existingOptionValue == null)
                {
                    existingOption.OptionValues.Add(optionValue);
                }
            }
        }
    }
}
