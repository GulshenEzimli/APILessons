using Application.Bases;

namespace Application.Features.Exceptions
{
	public class ProductTitleMustNotBeSameException : BaseExceptions
	{
		public ProductTitleMustNotBeSameException() : base("Məhsul başlığı artıq var.")
		{

		}
	}
}
