using MediatR;
using OnlineStore.BusinessLogic.Contracts.Dtos.Product;
using OnlineStore.BusinessLogic.Contracts.Interfaces;
using OnlineStore.BusinessLogic.Queries;
using OnlineStore.Data.Contracts.Models;

namespace OnlineStore.BusinessLogic.Handlers
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, PagedList<ProductDto>>
    {
        private readonly IProductService _productService;

        public GetProductsQueryHandler(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<PagedList<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            return await _productService.GetAllAsync(request.ProductGetAllRequestDto);
        }
    }
}
