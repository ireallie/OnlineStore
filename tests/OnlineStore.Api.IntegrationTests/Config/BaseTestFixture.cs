using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using OnlineStore.Data;

namespace OnlineStore.Api.IntegrationTests.Config
{
    public class BaseTestFixture
    {
        protected readonly CustomWebApplicationFactory<Program> _factory;
        protected ApplicationDbContext _context;

        public BaseTestFixture()
        {
            _factory = new CustomWebApplicationFactory<Program>();
        }

        [SetUp]
        public void SetUp()
        {
            var scope = _factory.Services.GetService<IServiceScopeFactory>()!.CreateScope();
            _context = scope.ServiceProvider.GetService<ApplicationDbContext>();

            _context.Database.EnsureDeleted();
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }
    }
}
