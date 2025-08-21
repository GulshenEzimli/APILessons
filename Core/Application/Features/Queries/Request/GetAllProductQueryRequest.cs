using Application.Features.Queries.Response;
using MediatR;

namespace Application.Features.Queries.Request
{
	public class GetAllProductQueryRequest : IRequest<IList<GetAllProductQueryResponse>>
	{
	}
}
