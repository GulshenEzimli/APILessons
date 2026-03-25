using Application.Features.Products.Command;
using FluentValidation;

namespace Application.Features.Products.Validators
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommandRequest>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithName("Məhsul ID").WithMessage("{PropertyName} 0-dan böyük olmalıdır.");
            RuleFor(x => x.Title).NotEmpty().WithName("Başlıq").WithMessage("{PropertyName} boş ola bilməz.");
            RuleFor(x => x.Description).NotEmpty().WithName("Açıqlama").WithMessage("{PropertyName}  boş ola bilməz.");
            RuleFor(x => x.BrandId).GreaterThan(0).WithName("Marka").WithMessage("{PropertyName} 0-dan böyük olmalıdır.");
            RuleFor(x => x.Price).GreaterThan(0).WithName("Qiymət").WithMessage("{PropertyName}  0-dan böyük olmalıdır.");
            RuleFor(x => x.Discount).GreaterThanOrEqualTo(0).WithName("Endirim dəyəri").WithMessage("{PropertyName}  0-dan kiçik olmamalıdır.");
            RuleFor(x => x.Categories).NotEmpty().Must(categories => categories.Any()).WithName("Kateqoriyalar").WithMessage("{PropertyName}  boş ola bilməz.");

        }
    }
}
