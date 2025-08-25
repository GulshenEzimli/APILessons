using Application.Bases;
using Application.Features.Exceptions;
using Domain.Entities;

namespace Application.Features.Rules
{
	public class ProductRules : BaseRules
	{
		public Task ProductTitleMustNotBeSame(IList<Product> products, string productTitle)
		{
			if(products.Any(x => x.Title == productTitle))
				throw new ProductTitleMustNotBeSameException();

			return Task.CompletedTask;
		}
	}
}
