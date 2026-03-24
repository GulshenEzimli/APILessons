using Application.Interfaces.AutoMapper;
using Application.Interfaces.UnitOfWorks;
using Domain.Entities;
using MediatR;

namespace Application.Features.Products.Command
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomMapper _mapper;
        public UpdateProductCommandHandler(IUnitOfWork unitOfWork, ICustomMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var productModel = _mapper.Map<UpdateProductCommandRequest, Product>(request);
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                var product = await _unitOfWork.BaseRepository<Product>().UpdateAsync(productModel);

                var productCategoryIds = (await _unitOfWork.BaseRepository<CategoryProduct>().GetAllAsync(cp => cp.ProductId == product.Id))
                                                           .Select(pc => pc.CategoryId).ToList();
                var deletedCategoryIds = productCategoryIds.Where(id => !request.Categories.Contains(id)).ToList();
                var addedCategoryIds = request.Categories.Where(id => !productCategoryIds.Contains(id)).ToList();

                foreach (var deletedCategoryId in deletedCategoryIds)
                {
                    var deleteProductCategory = await _unitOfWork.BaseRepository<CategoryProduct>().GetAsync(cp => cp.CategoryId == deletedCategoryId & cp.ProductId == product.Id);
                    await _unitOfWork.BaseRepository<CategoryProduct>().DeleteAsync(deleteProductCategory);
                }

                foreach (var addedCategoryId in addedCategoryIds)
                {
                    await _unitOfWork.BaseRepository<CategoryProduct>().CreateAsync(new CategoryProduct()
                    {
                        CategoryId = addedCategoryId,
                        ProductId = product.Id
                    });
                }

                await _unitOfWork.SaveAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);

                return new UpdateProductCommandResponse()
                {
                    SuccessMessage = "Product updated successfully.",
                    IsSuccess = true
                };
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);

                return new UpdateProductCommandResponse()
                {
                    SuccessMessage = "Product can not update!",
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
