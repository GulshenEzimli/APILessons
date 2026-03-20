using MediatR;

namespace Application.Features.Products.Command
{
    public class DeleteProductCommandRequest : IRequest<DeleteProductCommandResponse>   
    {
        public int Id { get; set; }
    }
}
