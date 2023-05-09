using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using OnlineStore.BusinessLogic.Contracts.Dtos.Product;
using OnlineStore.BusinessLogic.Contracts.Interfaces;
using OnlineStore.Data.Contracts.Models;

namespace OnlineStore.BusinessLogic.Services
{
    public class CachedProductService : IProductService
    {
        private const string ProductListCacheKey = "Products_v{0}_{1}_{2}_{3}_{4}";
        private const string ProductCacheKey = "Product_{0}_v{1}";

        private readonly Dictionary<string, SemaphoreSlim> _cacheLocks = new();
        private readonly IProductService _productService;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<CachedProductService> _logger;

        private long _cacheVersion = 0;

        public CachedProductService(
            IProductService productService,
            IMemoryCache memoryCache,
            ILogger<CachedProductService> logger)
        {
            _productService = productService;
            _memoryCache = memoryCache;
            _logger = logger;
        }

        public async Task<PagedList<ProductDto>> GetAllAsync(ProductGetAllRequestDto productGetAllRequestDto)
        {
            var cacheKey = string.Format(
                ProductListCacheKey, 
                _cacheVersion, 
                productGetAllRequestDto.PageNumber, 
                productGetAllRequestDto.PageSize,
                productGetAllRequestDto.Name,
                productGetAllRequestDto.OrderBy);

            _logger.LogInformation("Trying to fetch the list of products from cache.");

            var cacheLock = GetOrCreateCacheLock(cacheKey);
            await cacheLock.WaitAsync();

            try
            {
                if (_memoryCache.TryGetValue(cacheKey, out PagedList<ProductDto> products))
                {
                    _logger.LogInformation("Returning the product list from cache.");
                    return products;
                }

                _logger.LogInformation("Product list is not found in cache.");

                products = await _productService.GetAllAsync(productGetAllRequestDto);

                var cacheEntryOptions = GetCacheEntryOptions();

                _memoryCache.Set(cacheKey, products, cacheEntryOptions);

                return products;
            }
            finally
            {
                cacheLock.Release();
            }
        }

        public async Task<ProductDto> GetByIdAsync(int productId)
        {
            var cacheKey = string.Format(ProductCacheKey, productId, _cacheVersion);

            _logger.LogInformation("Trying to fetch the product with ID: {ProductId}, productId from cache", productId);

            var cacheLock = GetOrCreateCacheLock(cacheKey);
            await cacheLock.WaitAsync();

            try
            {
                if (_memoryCache.TryGetValue(cacheKey, out ProductDto product))
                {
                    _logger.LogInformation("Returning the product with ID: {ProductId} from cache.", productId);
                    return product;
                }

                _logger.LogInformation("Product with ID: {ProductId} is not found in cache.", productId);

                product = await _productService.GetByIdAsync(productId);

                var cacheEntryOptions = GetCacheEntryOptions();

                _memoryCache.Set(cacheKey, product, cacheEntryOptions);

                return product;
            }
            finally
            {
                cacheLock.Release();
            }
        }

        public async Task<ProductDto> CreateAsync(CreateProductDto createProductDto)
        {
            var result = await _productService.CreateAsync(createProductDto);

            Interlocked.Increment(ref _cacheVersion);

            return result;
        }

        public async Task<ProductDto> UpdateAsync(UpdateProductDto updateProductDto)
        {
            var result = await _productService.UpdateAsync(updateProductDto);

            Interlocked.Increment(ref _cacheVersion);

            return result;
        }

        public async Task DeleteAsync(int productId)
        {
            await _productService.DeleteAsync(productId);

            Interlocked.Increment(ref _cacheVersion);
        }

        private static MemoryCacheEntryOptions GetCacheEntryOptions()
        {
            return new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600));
        }

        private SemaphoreSlim GetOrCreateCacheLock(string cacheKey)
        {
            lock (_cacheLocks)
            {
                if (!_cacheLocks.TryGetValue(cacheKey, out SemaphoreSlim cacheLock))
                {
                    cacheLock = new SemaphoreSlim(1, 1);
                    _cacheLocks[cacheKey] = cacheLock;
                }

                return cacheLock;
            }
        }
    }
}
