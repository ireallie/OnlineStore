using OnlineStore.Data.Contracts.Entities.Products;
using OnlineStore.Data.Contracts.Interfaces;

namespace OnlineStore.Data.Repositories
{
    public class OptionRepository : Repository<Option>, IOptionRepository
    {
        public OptionRepository(ApplicationDbContext context) : base(context) { }

        public void Update(Option existingOption, Option updatedOption)
        {
            _context.Entry(existingOption).CurrentValues.SetValues(updatedOption);
            _context.Entry(existingOption).Property(o => o.CreatedDate).IsModified = false;
        }
    }
}
