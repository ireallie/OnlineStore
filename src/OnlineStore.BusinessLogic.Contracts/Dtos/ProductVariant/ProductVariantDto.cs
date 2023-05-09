using OnlineStore.BusinessLogic.Contracts.Dtos.ProductVariant.Base;

namespace OnlineStore.BusinessLogic.Contracts.Dtos.ProductVariant
{
    public class ProductVariantDto
        : ProductVariantEntityDto
        , IAuditedEntityDto
    {
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset? UpdatedDate { get; set; }
    }
}
