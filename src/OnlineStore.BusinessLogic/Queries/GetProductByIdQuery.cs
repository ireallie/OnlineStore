using MediatR;
using OnlineStore.BusinessLogic.Contracts.Dtos.Product;

namespace OnlineStore.BusinessLogic.Queries
{
    public class GetProductByIdQuery : IRequest<ProductDto>
    {
        public int Id { get; set; }
    }
}
