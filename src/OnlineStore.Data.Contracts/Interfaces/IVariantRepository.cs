using OnlineStore.Data.Contracts.Entities.Products;

namespace OnlineStore.Data.Contracts.Interfaces
{
    public interface IVariantRepository : IRepository<Variant>, IReadRepository<Variant>
    {
        void Update(Variant existingVariant, Variant updatedVariant);
    }
}
