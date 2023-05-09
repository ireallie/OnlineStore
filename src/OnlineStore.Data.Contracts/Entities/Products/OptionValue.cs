using OnlineStore.Data.Contracts.Entities.Auditing;

namespace OnlineStore.Data.Contracts.Entities.Products
{
    public class OptionValue : AuditableEntity
    {
        public int OptionValueId { get; set; }
        public string Value { get; set; }

        public int OptionId { get; set; }
        public virtual Option Option { get; set; }


        private readonly List<VariantOptionValue> _variantOptionValues = new();
        public virtual ICollection<VariantOptionValue> VariantOptionValues => _variantOptionValues;

        public OptionValue()
        {
            
        }
        public OptionValue(IEnumerable<VariantOptionValue> variantOptionValues)
        {
            _variantOptionValues = new(variantOptionValues);
        }
    }
}
