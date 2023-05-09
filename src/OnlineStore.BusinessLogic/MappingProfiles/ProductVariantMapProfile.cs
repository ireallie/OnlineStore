using AutoMapper;
using OnlineStore.Data.Contracts.Entities.Products;
using OnlineStore.BusinessLogic.Contracts.Dtos.ProductVariant.Base;
using OnlineStore.BusinessLogic.Contracts.Dtos.ProductVariant;
using OnlineStore.BusinessLogic.Contracts.Dtos.Product;
using OnlineStore.BusinessLogic.Contracts.Dtos.ProductOption;

namespace OnlineStore.BusinessLogic.MappingProfiles
{
    public class ProductVariantMapProfile : Profile
    {
        public ProductVariantMapProfile()
        {
            CreateEntityDtoMap<ProductVariantDto>()
               .ReverseMap()
               .ForMember(dto => dto.CreatedDate, options => options.MapFrom(model => model.CreatedDate))
               .ForMember(dto => dto.UpdatedDate, options => options.MapFrom(model => model.UpdatedDate))
               .ForMember(dto => dto.Choices, options => options.MapFrom(model => model.VariantOptionValues
                    .Select(v => v.OptionValue)
                    .ToDictionary(v => v.Option.Name, v => v.Value)))
                ;

            CreateBaseDtoMap<ProductVariantBaseDto>()
                .ReverseMap();

            CreateBaseDtoMap<CreateProductVariantDto>();
            CreateEntityDtoMap<UpdateProductVariantDto>();
        }

        private IMappingExpression<TDto, Variant> CreateEntityDtoMap<TDto>()
            where TDto : ProductVariantEntityDto
        {
            return CreateBaseDtoMap<TDto>()
                .ForMember(model => model.VariantId, options => options.MapFrom(dto => dto.Id))
                .ForMember(model => model.ProductId, options => options.MapFrom(dto => dto.ProductId))
                ;
        }

        private IMappingExpression<TDto, Variant> CreateBaseDtoMap<TDto>()
            where TDto : ProductVariantBaseDto
        {
            return CreateMap<TDto, Variant>()
                .ForMember(model => model.RegularPrice, options => options.MapFrom(dto => dto.RegularPrice))
                .ForMember(model => model.SalePrice, options => options.MapFrom(dto => dto.SalePrice))
                .ForMember(model => model.SKU, options => options.MapFrom(dto => dto.SKU))
                .ForMember(model => model.Quantity, options => options.MapFrom(dto => dto.Quantity))
                ;
        }
    }
}
