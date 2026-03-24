using Application.Dtos;
using Application.Interfaces.AutoMapper;
using Application.Interfaces.UnitOfWorks;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Products.Query
{
    public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQueryRequest, GetByIdProductQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomMapper _customMapper;
        public GetByIdProductQueryHandler(IUnitOfWork unitOfWork, ICustomMapper customMapper)
        {
            _unitOfWork = unitOfWork;
            _customMapper = customMapper;
        }
        public async Task<GetByIdProductQueryResponse> Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.BaseRepository<Product>().GetAsync(p => p.Id == request.Id, i => i.Include(p => p.Brand));

            _customMapper.AddMap<Brand, BrandDto>();
            var result = _customMapper.Map<Product, GetByIdProductQueryResponse>(product);

            return result;
        }
    }
}
