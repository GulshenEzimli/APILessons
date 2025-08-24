using Application.Dtos;
using MediatR;

namespace Application.Features.Commands.Requests
{
	public class CreateProductCommandRequest : IRequest<Unit>
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public decimal Discount { get; set; }
		public int BrandId { get; set; }
		public IList<int> CategoryIds { get; set; }
	}
}
