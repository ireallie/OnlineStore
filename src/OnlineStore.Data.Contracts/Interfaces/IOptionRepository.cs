using OnlineStore.Data.Contracts.Entities.Products;

namespace OnlineStore.Data.Contracts.Interfaces
{
    public interface IOptionRepository : IRepository<Option>, IReadRepository<Option>
    {
        void Update(Option existingOption, Option updatedOption);
    }
}
