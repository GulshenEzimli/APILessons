using Application.Interfaces.UnitOfWorks;
using Domain.Entities;
using MediatR;

namespace Application.Features.Products.Command
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Title = request.Title,
                Description = request.Description,
                ImagePath = request.ImagePath,
                Price = request.Price,
                BrandId = request.BrandId,
                Discount = request.Discount,
                CreatedDate = DateTime.Now,
                IsDeleted = false,
            };

            var result = new CreateProductCommandResponse();
            
            await _unitOfWork.BaseRepository<Product>().CreateAsync(product);
            var count = await _unitOfWork.SaveAsync();

            if (count == 1)
            {
                result.SuccessMessage = "Product created successfully.";
                result.IsSuccess = true;
            }
            else
            {
                result.SuccessMessage = "Product could not create!";
                result.IsSuccess = false;
            }
            return result;
        }
    }
}
