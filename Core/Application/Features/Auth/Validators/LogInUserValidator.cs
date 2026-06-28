using Application.Features.Auth.Commands;
using FluentValidation;

namespace Application.Features.Auth.Validators
{
    public class LogInUserValidator : AbstractValidator<LogInUserCommandRequest>
    {
        public LogInUserValidator()
        {
            RuleFor(u => u.UserName).NotNull().NotEmpty().WithMessage("Username bos ola bilmez.");
            RuleFor(u => u.Password).NotNull().NotEmpty().WithMessage("Password bos ola bilmez.");
        }
    }
}
