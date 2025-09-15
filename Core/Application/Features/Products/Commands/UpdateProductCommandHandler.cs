using Application.Bases;
using Application.Features.Products.Commands.Requests;
using Application.Interfaces.AutoMappers;
using Application.Interfaces.UnitOfWorks;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Products.Commands
{
	public class UpdateProductCommandHandler : BaseHandler,IRequestHandler<UpdateProductCommandRequest, Unit>
	{
		public UpdateProductCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
		{
		}
		public async Task<Unit> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
		{
			var productMap = mapper.Map<UpdateProductCommandRequest, Product>(request);
			var productCategories = await unitOfWork.GetReadRepository<CategoryProduct>()
													.GetAllAsync(cp => cp.ProductId == request.Id);
			var productCategoryIds = productCategories.Select(pc => pc.CategoryId).ToList();
													
			var deletedCategories = productCategoryIds.Where(pc => !request.CategoryIds.Contains(pc)).ToList();
			var addedCategories = request.CategoryIds.Where(c => !productCategoryIds.Contains(c)).ToList();

			foreach (var categoryId in deletedCategories)
				await unitOfWork.GetWriteRepository<CategoryProduct>().DeleteAsync(productCategories.First(pc => pc.CategoryId == categoryId && pc.ProductId == request.Id));

			foreach (var categoryId in addedCategories)
				await unitOfWork.GetWriteRepository<CategoryProduct>().AddAsync(new CategoryProduct 
				{
					ProductId = request.Id, 
					CategoryId = categoryId
				});

			await unitOfWork.GetWriteRepository<Product>().UpdateAsync(productMap);
			await unitOfWork.SaveAsync();

			return Unit.Value;
		}
	}
}
