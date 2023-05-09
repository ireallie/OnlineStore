using OnlineStore.Data.Contracts.Entities.Products;
using OnlineStore.Data.Contracts.Interfaces;

namespace OnlineStore.Data.Repositories
{
    public class VariantRepository : Repository<Variant>, IVariantRepository
    {
        public VariantRepository(ApplicationDbContext context) : base(context) { }

        public void Update(Variant existingVariant, Variant updatedVariant)
        {
            _context.Entry(existingVariant).CurrentValues.SetValues(updatedVariant);
            _context.Entry(existingVariant).Property(v => v.CreatedDate).IsModified = false;
        }
    }
}
