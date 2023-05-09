using OnlineStore.Data.Contracts.Entities.Auditing;

namespace OnlineStore.Data.Contracts.Entities.Products
{
    public class Option : AuditableEntity
    {
        public int OptionId { get; set; }
        public string Name { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
            

        private readonly List<OptionValue> _optionValues = new();
        public virtual ICollection<OptionValue> OptionValues => _optionValues;

        public Option()
        {
            
        }

        public Option(IEnumerable<OptionValue> optionValues)
        {
            _optionValues = new(optionValues);
        }
    }
}
