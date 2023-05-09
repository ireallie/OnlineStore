using AutoMapper;
using OnlineStore.BusinessLogic.Contracts.Dtos.Product.Base;
using OnlineStore.BusinessLogic.Contracts.Dtos.Product;
using OnlineStore.Data.Contracts.Entities.Products;
using OnlineStore.Data.Contracts.Models;

namespace OnlineStore.BusinessLogic.MappingProfiles
{
    public class ProductMapProfile : Profile
    {
        public ProductMapProfile()
        {
            CreateEntityDtoMap<ProductDto>()
               .ReverseMap()
               .ForMember(dto => dto.CreatedDate, options => options.MapFrom(model => model.CreatedDate))
               .ForMember(dto => dto.UpdatedDate, options => options.MapFrom(model => model.UpdatedDate))
               .ForMember(dto => dto.Options, options => options.MapFrom(model => model.Options))
               .ForMember(dto => dto.Variants, options => options.MapFrom(model => model.Variants));
            ;

            CreateBaseDtoMap<CreateProductDto>()
                .ForMember(model => model.Options, options => options.MapFrom(dto => dto.Options))
                .ForMember(model => model.Variants, options => options.MapFrom(dto => dto.Variants));

            CreateEntityDtoMap<UpdateProductDto>()
                .ForMember(model => model.Options, options => options.MapFrom(dto => dto.Options))
                .ForMember(model => model.Variants, options => options.MapFrom(dto => dto.Variants));

            CreateMap<ProductParameters, ProductGetAllRequestDto>()
                .ReverseMap();
        }
        private IMappingExpression<TDto, Product> CreateEntityDtoMap<TDto>()
            where TDto : ProductEntityDto
        {
            return CreateBaseDtoMap<TDto>()
                .ForMember(model => model.ProductId, options => options.MapFrom(dto => dto.Id))
                ;
        }

        private IMappingExpression<TDto, Product> CreateBaseDtoMap<TDto>()
            where TDto : ProductBaseDto
        {
            return CreateMap<TDto, Product>()
                .ForMember(model => model.Name, options => options.MapFrom(dto => dto.Name))
                .ForMember(model => model.Description, options => options.MapFrom(dto => dto.Description))
                .ForMember(model => model.Label, options => options.MapFrom(dto => dto.Label))
                .ForMember(model => model.IsVisible, options => options.MapFrom(dto => dto.IsVisible))
                ;
        }
    }
}
