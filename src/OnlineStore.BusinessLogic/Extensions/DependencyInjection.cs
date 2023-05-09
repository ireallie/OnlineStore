using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineStore.BusinessLogic.Contracts.Interfaces;
using OnlineStore.BusinessLogic.Services;
using OnlineStore.BusinessLogic.Validators.Product;
using OnlineStore.BusinessLogic.Validators.ProductOption;
using OnlineStore.BusinessLogic.Validators.ProductVariant;
using System.Reflection;

namespace OnlineStore.BusinessLogic.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessLogic(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddServices();
            services.AddAutoMapper();
            services.AddFluentValidation();
            services.AddMemoryCache();
            services.AddMediatr();

            return services;
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IProductService, ProductService>();
            services.Decorate<IProductService, CachedProductService>();
        }

        private static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        private static void AddMediatr(this IServiceCollection services)
        {
            services.AddMediatR(o => o.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        }

        private static void AddFluentValidation(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();

            services.AddValidatorsFromAssemblyContaining<CreateProductDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateProductDtoValidator>();

            services.AddValidatorsFromAssemblyContaining<CreateProductOptionDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateProductOptionDtoValidator>();

            services.AddValidatorsFromAssemblyContaining<CreateProductVariantDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateProductVariantDtoValidator>();
        }
    }
}
