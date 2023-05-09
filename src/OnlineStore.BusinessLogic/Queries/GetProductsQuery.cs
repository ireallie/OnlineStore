using MediatR;
using OnlineStore.BusinessLogic.Contracts.Dtos.Product;
using OnlineStore.Data.Contracts.Models;

namespace OnlineStore.BusinessLogic.Queries
{
    public class GetProductsQuery : IRequest<PagedList<ProductDto>>
    {
        public ProductGetAllRequestDto ProductGetAllRequestDto { get; set; }
    }
}
