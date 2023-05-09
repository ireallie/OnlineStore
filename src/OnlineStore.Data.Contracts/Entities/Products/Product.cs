using OnlineStore.Data.Contracts.Entities.Auditing;

namespace OnlineStore.Data.Contracts.Entities.Products
{
    public class Product : AuditableEntity
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Label { get; set; }
        public bool IsVisible { get; set; }


        private readonly List<Option> _options = new();
        public virtual ICollection<Option> Options => _options;


        private readonly List<Variant> _variants = new();
        public virtual ICollection<Variant> Variants => _variants;

        public Product()
        {
            
        }
        public Product(IEnumerable<Option> options, IEnumerable<Variant> variants)
        {
            _options = new(options);
            _variants = new(variants);
        }
    }
}
