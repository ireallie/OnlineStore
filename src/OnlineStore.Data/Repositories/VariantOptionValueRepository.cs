using OnlineStore.Data.Contracts.Entities.Products;
using OnlineStore.Data.Contracts.Interfaces;

namespace OnlineStore.Data.Repositories
{
    public class VariantOptionValueRepository : Repository<VariantOptionValue>, IVariantOptionValueRepository
    {
        public VariantOptionValueRepository(ApplicationDbContext context) : base(context) { }
    }
}
