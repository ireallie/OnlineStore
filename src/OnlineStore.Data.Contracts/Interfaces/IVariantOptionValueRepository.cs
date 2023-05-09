using OnlineStore.Data.Contracts.Entities.Products;

namespace OnlineStore.Data.Contracts.Interfaces
{
    public interface IVariantOptionValueRepository : IRepository<VariantOptionValue>, IReadRepository<VariantOptionValue>
    {
    }
}
