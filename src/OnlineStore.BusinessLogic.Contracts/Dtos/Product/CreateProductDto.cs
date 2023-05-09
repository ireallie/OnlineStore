using OnlineStore.BusinessLogic.Contracts.Dtos.Product.Base;
using OnlineStore.BusinessLogic.Contracts.Dtos.ProductOption;
using OnlineStore.BusinessLogic.Contracts.Dtos.ProductVariant;

namespace OnlineStore.BusinessLogic.Contracts.Dtos.Product
{
    public class CreateProductDto
        : ProductBaseDto
    {
        public IList<CreateProductOptionDto> Options { get; set; } = new List<CreateProductOptionDto>();
        public IList<CreateProductVariantDto> Variants { get; set; } = new List<CreateProductVariantDto>();
    }
}
