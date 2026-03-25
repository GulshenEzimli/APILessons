using Application.Bases;

namespace Application.Features.Products.Exceptions
{
    public class ProductTitleMustNotBeExistException : BaseExceptions
    {
        public ProductTitleMustNotBeExistException() : base("Product title must not be exist.")
        {
        }
    }
}
