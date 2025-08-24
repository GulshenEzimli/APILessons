using Application.Features.Commands.Requests;
using FluentValidation;

namespace Application.Validators
{
	public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommandRequest>
	{
		public UpdateProductCommandValidator()
		{
			RuleFor(p => p.Id).GreaterThan(0).WithName("Nömrə");
			RuleFor(p => p.Title).NotEmpty().WithName("Başlıq");
			RuleFor(p => p.Description).NotEmpty().WithName("Məlumat");
			RuleFor(p => p.Price).GreaterThan(0).WithName("Qiymət");
			RuleFor(p => p.BrandId).GreaterThan(0).WithName("Marka");
			RuleFor(p => p.Discount).GreaterThanOrEqualTo(0).WithName("Endirim");
			RuleFor(p => p.CategoryIds).Must(categoryIds => categoryIds.Any()).WithName("Kategoriyalar");
		}
	}
}
