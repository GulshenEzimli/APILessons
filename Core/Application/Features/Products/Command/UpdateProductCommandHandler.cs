using Application.Interfaces.UnitOfWorks;
using Domain.Entities;
using MediatR;

namespace Application.Features.Products.Command
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.BaseRepository<Product>().GetAsync(product => product.Id == request.Id);
            
            product.Title = request.Title;
            product.Description = request.Description;
            product.ImagePath= request.ImagePath;
            product.Price = request.Price;
            product.BrandId = request.BrandId;
            product.Discount = request.Discount;
            product.IsDeleted = request.IsDeleted;

            var updatedProduct = await _unitOfWork.BaseRepository<Product>().UpdateAsync(product);
            var count = await _unitOfWork.SaveAsync();

            var result = new UpdateProductCommandResponse();

            if(count == 1)
            {
                result.IsSuccess = true;
                result.SuccessMessage = "Product updated successfully.";
            }
            else
            {
                result.IsSuccess = false;
                result.SuccessMessage = "Product could not update!";
            }

            return result;
        }
    }
}
