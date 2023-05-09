using MediatR;
using OnlineStore.BusinessLogic.Commands;
using OnlineStore.BusinessLogic.Contracts.Dtos.Product;
using OnlineStore.BusinessLogic.Contracts.Interfaces;

namespace OnlineStore.BusinessLogic.Handlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDto>
    {
        private readonly IProductService _productService;

        public UpdateProductCommandHandler(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<ProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            return await _productService.UpdateAsync(request.UpdateProductDto);
        }
    }
}
