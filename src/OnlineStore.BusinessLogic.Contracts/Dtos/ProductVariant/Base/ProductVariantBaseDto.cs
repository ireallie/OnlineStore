namespace OnlineStore.BusinessLogic.Contracts.Dtos.ProductVariant.Base
{
    public class ProductVariantBaseDto
    {
        public decimal RegularPrice { get; set; }
        public decimal? SalePrice { get; set; }
        public string SKU { get; set; }
        public int? Quantity { get; set; }
        public IDictionary<string, string> Choices { get; set; } = new Dictionary<string, string>();
    }
}
