using Microsoft.EntityFrameworkCore;
using OnlineStore.Data.Contracts.Entities.Products;
using OnlineStore.Data.Contracts.Interfaces;
using OnlineStore.Data.Helpers;
using OnlineStore.Data.Repositories;
using OnlineStore.Data.UOW;

namespace OnlineStore.Data.UnitTests.Repositories
{
    public class DatabaseFixture : IDisposable
    {
        private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions;
        private ApplicationDbContext _context;
        public DatabaseFixture()
        {
            _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("OnlineStore.Data.UnitTests")
                .Options;
        }

        public async Task<IUnitOfWork> CreateUnitOfWork()
        {
            _context = new ApplicationDbContext(_dbContextOptions);
            _context.Database.EnsureDeleted();

            await SeedData(_context);

            var productSortHelper = new SortHelper<Product>();
            var productRepository = new ProductRepository(_context, productSortHelper);
            var variantRepository = new VariantRepository(_context);
            var optionRepository = new OptionRepository(_context);
            var optionValueRepository = new OptionValueRepository(_context);
            var variantOptionValueRepository = new VariantOptionValueRepository(_context);
            var uow = new UnitOfWork(
                _context, 
                productRepository, 
                variantRepository, 
                optionValueRepository, 
                optionRepository, 
                variantOptionValueRepository);

            return uow;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        private static async Task SeedData(ApplicationDbContext context)
        {        
            for (int i = 0; i < 3; i++)
            {
                var optionValue1 = new OptionValue
                {
                    Value = "OptionValue1",
                    CreatedDate = DateTimeOffset.UtcNow
                };

                var optionValue2 = new OptionValue
                {
                    Value = "OptionValue2",
                    CreatedDate = DateTimeOffset.UtcNow
                };

                var options = new List<Option>()
                {
                    new Option(new List<OptionValue>() { optionValue1, optionValue2 })
                    {
                        Name = "OptionName",
                        CreatedDate = DateTimeOffset.UtcNow
                    }
                };

                var variants = new List<Variant>()
                {
                    new Variant(new List<VariantOptionValue>() { new VariantOptionValue { OptionValue = optionValue1 } })
                    {
                        SKU = "SKU",
                        RegularPrice = 100,
                        CreatedDate = DateTimeOffset.UtcNow
                    },
                    new Variant(new List<VariantOptionValue>(){ new VariantOptionValue { OptionValue = optionValue2 }})
                    {
                        SKU = "SKU",
                        RegularPrice = 200,
                        CreatedDate = DateTimeOffset.UtcNow
                    }
                };

                var product = new Product(options, variants)
                {
                    Name = $"ProductName{i + 1}",
                    Description = "ProductDescription",
                    Label = "ProductLabel",
                    IsVisible = true,
                    CreatedDate = DateTimeOffset.UtcNow,
                };

                await context.Products.AddAsync(product);
            }

            await context.SaveChangesAsync();
        }
    }
}
