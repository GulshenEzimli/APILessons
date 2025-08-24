using Application.Features.Commands.Requests;
using FluentValidation;

namespace Application.Validators
{
	public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommandRequest>
	{
		public DeleteProductCommandValidator()
		{
			RuleFor(p => p.Id).GreaterThan(0).WithName("Nömrə");
		}
	}
}
