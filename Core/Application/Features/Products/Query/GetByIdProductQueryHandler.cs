using Application.Interfaces.UnitOfWorks;
using Domain.Entities;
using MediatR;

namespace Application.Features.Products.Query
{
    public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQueryRequest, GetByIdProductQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetByIdProductQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<GetByIdProductQueryResponse> Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.BaseRepository<Product>().GetAsync(p => p.Id == request.Id);
            var result = new GetByIdProductQueryResponse
            {
                Title = product.Title,
                Description = product.Description,
                Price = product.Price,
                BrandId = product.BrandId,
                Discount = product.Discount,
                CreatedDate = product.CreatedDate,
                IsDeleted = product.IsDeleted
            };

            return result;
        }
    }
}
