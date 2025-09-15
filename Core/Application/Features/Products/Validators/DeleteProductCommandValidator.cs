using Application.Features.Products.Commands.Requests;
using FluentValidation;

namespace Application.Features.Products.Validators
{
	public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommandRequest>
	{
		public DeleteProductCommandValidator()
		{
			RuleFor(p => p.Id).GreaterThan(0).WithName("Nömrə");
		}
	}
}
