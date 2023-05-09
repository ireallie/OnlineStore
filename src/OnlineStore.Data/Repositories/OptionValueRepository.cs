using OnlineStore.Data.Contracts.Entities.Products;
using OnlineStore.Data.Contracts.Interfaces;

namespace OnlineStore.Data.Repositories
{
    public class OptionValueRepository : Repository<OptionValue>, IOptionValueRepository
    {
        public OptionValueRepository(ApplicationDbContext context) : base(context) { }
    }
}
