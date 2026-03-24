using Application.Interfaces.AutoMapper;
using Application.Interfaces.UnitOfWorks;
using Domain.Entities;
using MediatR;

namespace Application.Features.Products.Command
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomMapper _customMapper;
        public CreateProductCommandHandler(IUnitOfWork unitOfWork, ICustomMapper customMapper)
        {
            _unitOfWork = unitOfWork;
            _customMapper = customMapper;
        }
        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = _customMapper.Map<CreateProductCommandRequest, Product>(request);
            product.IsDeleted = false;
            product.CreatedDate = DateTime.Now;

            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                await _unitOfWork.BaseRepository<Product>().CreateAsync(product);
                await _unitOfWork.SaveAsync(cancellationToken);

                foreach (var categoryId in request.Categories)
                {
                    await _unitOfWork.BaseRepository<CategoryProduct>().CreateAsync(new()
                    {
                        ProductId = product.Id,
                        CategoryId = categoryId
                    });
                }

                await _unitOfWork.SaveAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);

                return new CreateProductCommandResponse()
                {
                    SuccessMessage = "Product created successfully!",
                    IsSuccess = true
                };
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);

                return new CreateProductCommandResponse()
                {
                    SuccessMessage = "Product can not create!",
                    IsSuccess = false
                };
            }
            finally
            {
                await _unitOfWork.DisposeTransactionAsync();
            }
           
        }
    }
}
