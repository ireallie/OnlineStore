using MediatR;

namespace OnlineStore.BusinessLogic.Commands
{
    public class DeleteProductCommand : IRequest
    {
        public int Id { get; set; }
    }
}
