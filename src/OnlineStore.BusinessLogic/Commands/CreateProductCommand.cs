using MediatR;
using OnlineStore.BusinessLogic.Contracts.Dtos.Product;

namespace OnlineStore.BusinessLogic.Commands
{
    public class CreateProductCommand : IRequest<ProductDto>
    {
       public CreateProductDto CreateProductDto { get; set; }
    }
}
