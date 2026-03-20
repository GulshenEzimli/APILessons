using MediatR;

namespace Application.Features.Products.Query
{
    public class GetAllProductQueryRequest :IRequest<IList<GetAllProductQueryResponse>>
    {
    }
}
