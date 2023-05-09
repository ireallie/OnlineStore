using MediatR;
using OnlineStore.BusinessLogic.Commands;
using OnlineStore.BusinessLogic.Contracts.Interfaces;

namespace OnlineStore.BusinessLogic.Handlers
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IProductService _productService;

        public DeleteProductCommandHandler(IProductService productService)
        {
            _productService = productService;
        }
        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            await _productService.DeleteAsync(request.Id);
        }
    }
}
