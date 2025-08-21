using Application.Features.Queries.Request;
using Application.Features.Queries.Response;
using Application.Interfaces.UnitOfWorks;
using Domain.Entities;
using MediatR;

namespace Application.Features.Handlers.Query
{
	public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, IList<GetAllProductQueryResponse>>
	{
		private readonly IUnitOfWork _unitOfWork;
		public GetAllProductQueryHandler(IUnitOfWork unitOfwork)
		{
			_unitOfWork = unitOfwork;
		}
		public async Task<IList<GetAllProductQueryResponse>> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
		{
			var products = await _unitOfWork.GetReadRepository<Product>().GetAllAsync();

			var result = products.Select(p => new GetAllProductQueryResponse
			{
				Title = p.Title,
				Description = p.Description,
				Price = p.Price * (1 - p.Discount/100),
				Discount = p.Discount,
			}).ToList();

			return result;
		}
	}
}
