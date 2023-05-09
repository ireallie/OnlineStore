namespace OnlineStore.BusinessLogic.Contracts.Dtos.Product.Base
{
    public class ProductBaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string SKU { get; set; }
        public decimal RegularPrice { get; set; }
        public decimal? SalePrice { get; set; }
        public string Label { get; set; }
        public bool IsVisible { get; set; }
        public int? Quantity { get; set; } 
    }
}
