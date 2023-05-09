using MediatR;
using OnlineStore.BusinessLogic.Contracts.Dtos.Product;

namespace OnlineStore.BusinessLogic.Commands
{
    public class UpdateProductCommand : IRequest<ProductDto>
    {
        public UpdateProductDto UpdateProductDto { get; set; }
    }
}
