using Application.Interfaces.UnitOfWorks;
using Domain.Entities;
using MediatR;

namespace Application.Features.Products.Query
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, IList<GetAllProductQueryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllProductQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IList<GetAllProductQueryResponse>> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {
            var products = await _unitOfWork.BaseRepository<Product>().GetAllAsync();

            var result = products.Select(product => new GetAllProductQueryResponse
            {
                Title = product.Title,
                Description = product.Description,
                Price = product.Price,
                BrandId = product.BrandId,
                Discount = product.Discount,
                CreatedDate = product.CreatedDate,
                IsDeleted = product.IsDeleted
            }).ToList();

            return result;
        }
    }
}
