using OnlineStore.BusinessLogic.Contracts.Dtos.Product.Base;
using OnlineStore.BusinessLogic.Contracts.Dtos.ProductOption;
using OnlineStore.BusinessLogic.Contracts.Dtos.ProductVariant;

namespace OnlineStore.BusinessLogic.Contracts.Dtos.Product
{
    public class UpdateProductDto
        : ProductEntityDto
    {
        public IList<UpdateProductOptionDto> Options { get; set; } = new List<UpdateProductOptionDto>();
        public IList<UpdateProductVariantDto> Variants { get; set; } = new List<UpdateProductVariantDto>();
    }
}
