namespace OnlineStore.BusinessLogic.Contracts.Dtos.ProductOption.Base
{
    public class ProductOptionEntityDto
        : ProductOptionBaseDto
        , IEntityDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
    }
}
