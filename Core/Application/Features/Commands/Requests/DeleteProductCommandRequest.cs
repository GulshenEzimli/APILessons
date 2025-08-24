using MediatR;

namespace Application.Features.Commands.Requests
{
	public class DeleteProductCommandRequest : IRequest<Unit>
	{
		public int Id { get; set; }
	}
}
