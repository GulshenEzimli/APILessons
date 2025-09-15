using Application.Bases;

namespace Application.Features.Products.Exceptions
{
	public class ProductTitleMustNotBeSameException : BaseExceptions
	{
		public ProductTitleMustNotBeSameException() : base("Məhsul başlığı artıq var.")
		{

		}
	}
}
