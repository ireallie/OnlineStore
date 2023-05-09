using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using OnlineStore.BusinessLogic.Contracts.Dtos.Product;
using OnlineStore.BusinessLogic.Contracts.Interfaces;
using OnlineStore.BusinessLogic.Exceptions;
using OnlineStore.BusinessLogic.MappingProfiles;
using OnlineStore.BusinessLogic.Services;
using OnlineStore.Data.Contracts.Entities.Products;
using OnlineStore.Data.Contracts.Interfaces;
using Xunit;

namespace OnlineStore.BusinessLogic.UnitTests.Services
{
    public class ProductServiceTests
    {
        private const int ProductId = 1;

        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly Product _product;
        private readonly UpdateProductDto _updateProductDto;

        private readonly Mock<IUnitOfWorkFactory> _unitOfWorkFactory;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IProductRepository> _productRepository;
        private readonly Mock<IVariantRepository> _variantRepository;
        private readonly Mock<ILogger<ProductService>> _logger;

        public ProductServiceTests()
        {
            _product = new Product
            {
                ProductId = ProductId,
                Name = "Name",
                Description = "Description",
                Label = "Label",
                IsVisible = true,
                CreatedDate = DateTimeOffset.UtcNow
            };

            _updateProductDto = new UpdateProductDto()
            {
                Id = ProductId,
                Name = "UpdateProductDtoName",
                Description = "UpdateProductDtoDescription",
                SKU = "UpdateProductDtoSKU",
                RegularPrice = 999,
                SalePrice = null,
                Label = "UpdateProductDtoLabel",
                IsVisible = true,
                Quantity = null
            };

            _unitOfWork = new Mock<IUnitOfWork>();
            _productRepository = new Mock<IProductRepository>();
            _variantRepository = new Mock<IVariantRepository>();
            _logger = new Mock<ILogger<ProductService>>();

            _unitOfWork
                .Setup(uow => uow.ProductRepository)
                .Returns(() => _productRepository.Object);

            _unitOfWork
                .Setup(uow => uow.VariantRepository)
                .Returns(() => _variantRepository.Object);

            _productRepository
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_product);

            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(
                    typeof(ProductMapProfile),
                    typeof(ProductOptionMapProfile),
                    typeof(ProductOptionValueMapProfile),
                    typeof(ProductVariantMapProfile));
            })
            .CreateMapper();

            _productService = new ProductService(_logger.Object, _unitOfWorkFactory.Object, _mapper);
        }

        [Fact]
        public async Task GetAllAsync_WhenSuccess_ReturnsProductDtoList()
        {
            // Arrange
            var products = new List<Product>() { _product, _product };

            _productRepository
                .Setup(repo => repo.GetListAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(products);

            // Act
            var result = await _productService.GetAllAsync(It.IsAny<ProductGetAllRequestDto>());

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetByIdAsync_WhenSuccess_ReturnsProductDto()
        {
            // Arrange
            _productRepository
                .Setup(repo => repo.GetProductWithDetailsAsync(It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_product);

            // Act
            var result = await _productService.GetByIdAsync(ProductId);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetByIdAsync_WhenProductDoesNotExist_ThrowsNotFoundException()
        {
            // Arrange
            _productRepository
                .Setup(repo => repo.GetProductWithDetailsAsync(It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Product)null);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _productService.GetByIdAsync(ProductId));
        }

        [Fact]
        public async Task CreateAsync_Success()
        {
            // Arrange
            _productRepository
                .Setup(repo => repo.AddAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()));

            var createProductDto = new CreateProductDto()
            {
                Name = "CreateProductDtoName",
                Description = "CreateProductDtoDescription",
                SKU = "CreateProductDtoSKU",
                RegularPrice = 999,
                SalePrice = null,
                Label = "CreateProductDtoLabel",
                IsVisible = true,
                Quantity = null 
            };

            // Act
            var result = await _productService.CreateAsync(createProductDto);

            // Assert
            Assert.IsType<ProductDto>(result);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateAsync_Success()
        {
            // Arrange
            _productRepository
                .Setup(repo => repo.Update(It.IsAny<Product>()))
                .Returns(_product);

            // Act
            var result = await _productService.UpdateAsync(_updateProductDto);

            // Assert
            Assert.IsType<ProductDto>(result);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateAsync_WhenProductDoesNotExist_ThrowsNotFoundException()
        {
            // Arrange
            _productRepository
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Product)null!);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _productService.UpdateAsync(_updateProductDto));
        }

        [Fact]
        public async Task DeleteAsync_WhenSuccess_CallsRepositoryDelete()
        {
            // Act
            await _productService.DeleteAsync(ProductId);

            // Assert
            _productRepository.Verify(x => x.Delete(It.IsAny<Product>()), Times.Once());
        }

        [Fact]
        public async Task DeleteAsync_WhenProductDoesNotExist_ThrowsNotFoundException()
        {
            // Arrange
            _productRepository
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Product)null!);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _productService.DeleteAsync(ProductId));
        }
    }
}
