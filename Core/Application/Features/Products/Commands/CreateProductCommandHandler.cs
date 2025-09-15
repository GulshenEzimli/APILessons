using Application.Bases;
using Application.Features.Products.Commands.Requests;
using Application.Features.Products.Rules;
using Application.Interfaces.AutoMappers;
using Application.Interfaces.UnitOfWorks;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Products.Commands
{
	public class CreateProductCommandHandler : BaseHandler, IRequestHandler<CreateProductCommandRequest, Unit>
	{
		private readonly ProductRules productRules;

		public CreateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper,IHttpContextAccessor httpContextAccessor, ProductRules productRules) : base(mapper, unitOfWork, httpContextAccessor)
		{
			this.productRules = productRules;
		}
		public async Task<Unit> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
		{
			var productMap = mapper.Map<CreateProductCommandRequest, Product>(request);

			var products = await unitOfWork.GetReadRepository<Product>().GetAllAsync();
			await productRules.ProductTitleMustNotBeSame(products, request.Title);

			await unitOfWork.GetWriteRepository<Product>().AddAsync(productMap);
			int addedproduct = await unitOfWork.SaveAsync();

			if(addedproduct > 0)
			{
				foreach (var categoryId in request.CategoryIds)
				{
					await unitOfWork.GetWriteRepository<CategoryProduct>().AddAsync(new CategoryProduct 
					{
						ProductId = productMap.Id, 
						CategoryId = categoryId 
					});
				}
				await unitOfWork.SaveAsync();
			}

			return Unit.Value;
		}
	}
}
