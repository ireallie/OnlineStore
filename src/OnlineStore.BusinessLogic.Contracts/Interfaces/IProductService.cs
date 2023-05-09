using OnlineStore.BusinessLogic.Contracts.Dtos.Product;
using OnlineStore.Data.Contracts.Models;

namespace OnlineStore.BusinessLogic.Contracts.Interfaces
{
    public interface IProductService
    {
        Task<PagedList<ProductDto>> GetAllAsync(ProductGetAllRequestDto productGetAllRequestDto);
        Task<ProductDto> GetByIdAsync(int productId);
        Task<ProductDto> CreateAsync(CreateProductDto createProductDto);
        Task<ProductDto> UpdateAsync(UpdateProductDto updateProductDto);
        Task DeleteAsync(int productId);
    }
}
