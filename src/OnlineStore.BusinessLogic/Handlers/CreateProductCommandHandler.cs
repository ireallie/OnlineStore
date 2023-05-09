using MediatR;
using OnlineStore.BusinessLogic.Commands;
using OnlineStore.BusinessLogic.Contracts.Dtos.Product;
using OnlineStore.BusinessLogic.Contracts.Interfaces;

namespace OnlineStore.BusinessLogic.Handlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
    {
        private readonly IProductService _productService;

        public CreateProductCommandHandler(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            return await _productService.CreateAsync(request.CreateProductDto);
        }
    }
}
