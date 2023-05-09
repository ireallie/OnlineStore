using OnlineStore.Data.Contracts.Entities.Products;

namespace OnlineStore.Data.Contracts.Interfaces
{
    public interface IOptionValueRepository : IRepository<OptionValue>, IReadRepository<OptionValue>
    {
    }
}
