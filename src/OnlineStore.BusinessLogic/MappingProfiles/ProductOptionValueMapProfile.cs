using AutoMapper;
using OnlineStore.BusinessLogic.Contracts.Dtos.ProductOption.Base;
using OnlineStore.Data.Contracts.Entities.Products;

namespace OnlineStore.BusinessLogic.MappingProfiles
{
    public class ProductOptionValueMapProfile : Profile
    {
        public ProductOptionValueMapProfile()
        {
            CreateMap<ProductOptionEntityDto, IEnumerable<OptionValue>>()
                .ConstructUsing(dto => dto.Values.Select(str => new OptionValue { Value = str }));

            CreateMap<string, OptionValue>()
                .ConstructUsing(str => new OptionValue { Value = str });
        }
    }
}
