using Application.Bases;

namespace Application.Features.Products.Exceptions
{
	public class ProductTitleMustNotBeSameException : BaseException
	{
		public ProductTitleMustNotBeSameException() : base("Məhsul başlığı artıq var.")
		{

		}
	}
}
