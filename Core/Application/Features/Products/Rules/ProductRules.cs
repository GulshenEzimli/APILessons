using Application.Bases;
using Application.Features.Products.Exceptions;
using Domain.Entities;

namespace Application.Features.Products.Rules
{
    public class ProductRules : BaseRules
    {
        public Task ProductTitleMustNotBeExist(IList<Product> products, string requestTitle)
        {
            if (products.Any(x => x.Title == requestTitle))
                throw new ProductTitleMustNotBeExistException();

            return Task.CompletedTask;
        }
    }
}
