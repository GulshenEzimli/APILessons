using Application.Interfaces.UnitOfWorks;
using Domain.Entities;
using MediatR;

namespace Application.Features.Products.Command
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest, DeleteProductCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DeleteProductCommandResponse> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.BaseRepository<Product>().GetAsync(p => p.Id == request.Id);
            var result = new DeleteProductCommandResponse();

            await _unitOfWork.BaseRepository<Product>().DeleteAsync(product);
            var count = await _unitOfWork.SaveAsync();

            if (count == 1)
            {
                result.SuccessMessage = "Product deleted successfully.";
                result.IsSuccess = true;
            }
            else
            {
                result.SuccessMessage = "Product could not delete!";
                result.IsSuccess = false;
            }

            return result;
        }
    }
}
