using OnlineStore.Data.Contracts.Entities.Auditing;

namespace OnlineStore.Data.Contracts.Entities.Products
{
    public class Variant : AuditableEntity
    {
        public int VariantId { get; set; }
        public string SKU { get; set; }
        public decimal RegularPrice { get; set; }
        public decimal? SalePrice { get; set; }
        public int? Quantity { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }


        private readonly List<VariantOptionValue> _variantOptionValues = new();
        public virtual ICollection<VariantOptionValue> VariantOptionValues => _variantOptionValues;

        public Variant()
        {
            
        }
        public Variant(IEnumerable<VariantOptionValue> variantOptionValues)
        {
            _variantOptionValues = new(variantOptionValues);
        }
    }
}
