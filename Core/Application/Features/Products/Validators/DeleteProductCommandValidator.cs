using Application.Features.Products.Command;
using FluentValidation;

namespace Application.Features.Products.Validators
{
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommandRequest>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithName("Məhsul ID").WithMessage("{PropertyName} 0-dan böyük olmalıdır.");

        }
    }
}
