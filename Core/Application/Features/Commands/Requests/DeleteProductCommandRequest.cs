using MediatR;

namespace Application.Features.Commands.Requests
{
	public class DeleteProductCommandRequest : IRequest
	{
		public int Id { get; set; }
	}
}
