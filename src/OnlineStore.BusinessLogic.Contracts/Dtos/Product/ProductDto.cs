using OnlineStore.BusinessLogic.Contracts.Dtos.Product.Base;
using OnlineStore.BusinessLogic.Contracts.Dtos.ProductOption;
using OnlineStore.BusinessLogic.Contracts.Dtos.ProductVariant;

namespace OnlineStore.BusinessLogic.Contracts.Dtos.Product
{
    public class ProductDto
        : ProductEntityDto
        , IAuditedEntityDto
    {
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset? UpdatedDate { get; set; }

        public IList<ProductOptionDto> Options { get; set; } = new List<ProductOptionDto>();
        public IList<ProductVariantDto> Variants { get; set; } = new List<ProductVariantDto>();
    }
}
