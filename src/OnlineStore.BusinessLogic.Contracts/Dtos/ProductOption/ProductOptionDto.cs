using OnlineStore.BusinessLogic.Contracts.Dtos.ProductOption.Base;

namespace OnlineStore.BusinessLogic.Contracts.Dtos.ProductOption
{
    public class ProductOptionDto
        : ProductOptionEntityDto
        , IAuditedEntityDto
    {
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset? UpdatedDate { get; set; }
    }
}
