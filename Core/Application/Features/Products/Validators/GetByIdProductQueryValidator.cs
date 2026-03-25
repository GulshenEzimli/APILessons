using Application.Features.Products.Query;
using FluentValidation;

namespace Application.Features.Products.Validators
{
    public class GetByIdProductQueryValidator : AbstractValidator<GetByIdProductQueryRequest>
    {
        public GetByIdProductQueryValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithName("Məhsul ID").WithMessage("{PropertyName} 0-dan böyük olmalıdır.");

        }
    }
}
