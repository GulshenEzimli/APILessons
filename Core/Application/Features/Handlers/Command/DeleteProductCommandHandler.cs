using Application.Features.Commands.Requests;
using Application.Interfaces.UnitOfWorks;
using Domain.Entities;
using MediatR;

namespace Application.Features.Handlers.Command
{
	public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest>
	{
		private readonly IUnitOfWork unitOfWork;

		public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}
		public async Task Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
		{
			var product = await unitOfWork.GetReadRepository<Product>().GetAsync(p => p.Id == request.Id && !p.IsDeleted);
			product.IsDeleted = true;

			await unitOfWork.GetWriteRepository<Product>().UpdateAsync(product);
			await unitOfWork.SaveAsync();
		}
	}
}
