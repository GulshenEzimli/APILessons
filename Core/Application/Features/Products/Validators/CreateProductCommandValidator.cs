using Application.Features.Products.Commands.Requests;
using FluentValidation;

namespace Application.Features.Products.Validators
{
	public class CreateProductCommandValidator : AbstractValidator<CreateProductCommandRequest>
	{
		public CreateProductCommandValidator()
		{
			RuleFor(p => p.Title).NotEmpty().WithName("Başlıq");
			RuleFor(p => p.Description).NotEmpty().WithName("Məlumat");
			RuleFor(p => p.Price).GreaterThan(0).WithName("Qiymət");
			RuleFor(p => p.BrandId).GreaterThan(0).WithName("Marka");
			RuleFor(p => p.Discount).GreaterThanOrEqualTo(0).WithName("Endirim");
			RuleFor(p => p.CategoryIds).Must(categoryIds => categoryIds.Any()).WithName("Kategoriyalar");
		}
	}
}
