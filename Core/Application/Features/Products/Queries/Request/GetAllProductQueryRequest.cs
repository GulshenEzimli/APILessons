using Application.Features.Products.Queries.Response;
using MediatR;

namespace Application.Features.Products.Queries.Request
{
	public class GetAllProductQueryRequest : IRequest<IList<GetAllProductQueryResponse>>
	{
	}
}
