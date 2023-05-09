namespace OnlineStore.BusinessLogic.Contracts.Dtos.ProductVariant.Base
{
    public class ProductVariantEntityDto
        : ProductVariantBaseDto
        , IEntityDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
    }
}
