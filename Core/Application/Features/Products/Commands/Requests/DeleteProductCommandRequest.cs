using MediatR;

namespace Application.Features.Products.Commands.Requests
{
	public class DeleteProductCommandRequest : IRequest<Unit>
	{
		public int Id { get; set; }
	}
}
