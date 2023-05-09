namespace OnlineStore.BusinessLogic.Contracts.Dtos.Product.Base
{
    public class ProductEntityDto
        : ProductBaseDto
        , IEntityDto
    {
        public int Id { get; set; }
    }
}
