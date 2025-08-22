using Application.Dtos;
using Application.Features.Queries.Request;
using Application.Features.Queries.Response;
using Application.Interfaces.AutoMappers;
using Application.Interfaces.UnitOfWorks;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Handlers.Query
{
	public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, IList<GetAllProductQueryResponse>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper mapper;

		public GetAllProductQueryHandler(IUnitOfWork unitOfwork, IMapper _mapper)
		{
			_unitOfWork = unitOfwork;
			mapper = _mapper;
		}


		public async Task<IList<GetAllProductQueryResponse>> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
		{
			var products = await _unitOfWork.GetReadRepository<Product>().GetAllAsync(include : x => x.Include(t => t.Brand));
			mapper.Map<Brand, BrandDto>(new Brand());

			var response = mapper.Map<Product, GetAllProductQueryResponse>(products);

			return response;
		}
	}
}
