namespace OnlineStore.Data.Contracts.Entities.Products
{
    public class VariantOptionValue
    {
        public int VariantId { get; set; }
        public virtual Variant Variant { get; set; }

        public int OptionValueId { get; set; }
        public virtual OptionValue OptionValue { get; set; }
    }
}
