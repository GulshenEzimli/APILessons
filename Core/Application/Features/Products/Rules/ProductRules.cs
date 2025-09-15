using Application.Bases;
using Application.Features.Products.Exceptions;
using Domain.Entities;

namespace Application.Features.Products.Rules
{
	public class ProductRules : BaseRule
	{
		public Task ProductTitleMustNotBeSame(IList<Product> products, string productTitle)
		{
			if(products.Any(x => x.Title == productTitle))
				throw new ProductTitleMustNotBeSameException();

			return Task.CompletedTask;
		}
	}
}
