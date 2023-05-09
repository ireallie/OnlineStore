using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineStore.Data.Contracts.Entities.Products;
using OnlineStore.Data.Contracts.Interfaces;
using OnlineStore.Data.Helpers;
using OnlineStore.Data.Interceptors;
using OnlineStore.Data.Repositories;
using OnlineStore.Data.UOW;

namespace OnlineStore.Data.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabase(configuration);
            services.AddUnitOfWork();
            services.AddRepositories();
            services.AddInterceptors();
            services.AddHelpers();

            return services;
        }

        private static void AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IUnitOfWorkFactory, UnitOfWorkFactory>();
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IReadRepository<>), typeof(Repository<>));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IVariantRepository, VariantRepository>();
            services.AddScoped<IVariantOptionValueRepository, VariantOptionValueRepository>();
            services.AddScoped<IOptionRepository, OptionRepository>();
            services.AddScoped<IOptionValueRepository, OptionValueRepository>();
        }

        private static void AddHelpers(this IServiceCollection services)
        {
            services.AddScoped<ISortHelper<Product>, SortHelper<Product>>();
        }

        private static void AddInterceptors(this IServiceCollection services)
        {
            services.AddSingleton<AuditableEntitiesInterceptor>();
        }

        private static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(
                (sp, optionsBuilder) =>
            {
                var interceptor = sp.GetService<AuditableEntitiesInterceptor>();

                optionsBuilder.UseSqlServer(configuration.GetConnectionString("MainDatabase"),
                        o => o.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
                            .AddInterceptors(interceptor);
            });
        }
    }
}
