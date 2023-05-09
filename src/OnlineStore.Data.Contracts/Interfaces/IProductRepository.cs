using OnlineStore.Data.Contracts.Entities.Products;
using OnlineStore.Data.Contracts.Models;

namespace OnlineStore.Data.Contracts.Interfaces
{
    public interface IProductRepository : IRepository<Product>, IReadRepository<Product>
    {
        Task<PagedList<Product>> GetProductsAsync(ProductParameters productParameters);
        Task<Product> GetProductWithDetailsAsync<TId>(TId id, bool asNoTracking = true, CancellationToken cancellationToken = default);
        void Update(Product existingProduct, Product updatedProduct);

        new Product Update(Product product);
    }
}
