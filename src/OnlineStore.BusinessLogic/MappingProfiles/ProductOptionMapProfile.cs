using AutoMapper;
using OnlineStore.Data.Contracts.Entities.Products;
using OnlineStore.BusinessLogic.Contracts.Dtos.ProductOption;
using OnlineStore.BusinessLogic.Contracts.Dtos.ProductOption.Base;

namespace OnlineStore.BusinessLogic.MappingProfiles
{
    public class ProductOptionMapProfile : Profile
    {
        public ProductOptionMapProfile()
        {
            CreateEntityDtoMap<ProductOptionDto>()
               .ReverseMap()
               .ForMember(dto => dto.CreatedDate, options => options.MapFrom(model => model.CreatedDate))
               .ForMember(dto => dto.UpdatedDate, options => options.MapFrom(model => model.UpdatedDate))
               .ForMember(dto => dto.Values, options => options.MapFrom(model => model.OptionValues.Select(o => o.Value).ToList()))
               ;

            CreateBaseDtoMap<ProductOptionBaseDto>()
                .ReverseMap();

            CreateBaseDtoMap<CreateProductOptionDto>();

            CreateEntityDtoMap<UpdateProductOptionDto>()
                .ForMember(dest => dest.OptionValues, opt => opt.MapFrom(src => src.Values.Select(val => new OptionValue { Value = val, OptionId = src.Id })));
        }

        private IMappingExpression<TDto, Option> CreateEntityDtoMap<TDto>()
            where TDto : ProductOptionEntityDto
        {
            return CreateBaseDtoMap<TDto>()
                .ForMember(model => model.OptionId, options => options.MapFrom(dto => dto.Id))
                .ForMember(model => model.ProductId, options => options.MapFrom(dto => dto.ProductId))
                ;
        }

        private IMappingExpression<TDto, Option> CreateBaseDtoMap<TDto>()
            where TDto : ProductOptionBaseDto
        {
            return CreateMap<TDto, Option>()
                .ForMember(model => model.Name, options => options.MapFrom(dto => dto.Name))
                .ForMember(model => model.OptionValues, options => options.MapFrom(dto => dto.Values))
                .AfterMap((src, dest) =>
                {
                    foreach (var optionValue in dest.OptionValues)
                    {
                        optionValue.Option = dest;
                    }
                });
            ;
        }
    }
}
