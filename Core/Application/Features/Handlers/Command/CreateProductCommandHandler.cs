using Application.Dtos;
using Application.Features.Commands.Requests;
using Application.Interfaces.AutoMappers;
using Application.Interfaces.UnitOfWorks;
using Domain.Entities;
using MediatR;

namespace Application.Features.Handlers.Command
{
	public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, Unit>
	{
		private readonly IUnitOfWork unitOfWork;
		private readonly IMapper mapper;

		public CreateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			this.unitOfWork = unitOfWork;
			this.mapper = mapper;
		}
		public async Task<Unit> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
		{
			var productMap = mapper.Map<CreateProductCommandRequest, Product>(request);

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
