using Application.Features.Auth.Commands;
using FluentValidation;

namespace Application.Features.Auth.Validators
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserCommandRequest>
    {
        public RegisterUserValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required.")
                .MinimumLength(3).WithMessage("Username must be at least 3 characters long.")
                .MaximumLength(50);

            RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Fullname is required.")
            .MinimumLength(3).WithMessage("Fullname must be at least 3 characters long.")
            .MaximumLength(50);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.")
                .MinimumLength(13);

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Length(10).WithMessage("Invalid phone number format.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
                .MaximumLength(100);

            RuleForEach(x => x.Roles)
                .NotEmpty().WithMessage("Role cannot be empty.")
                .MinimumLength(3).WithMessage("Role must be at least 3 characters long.")
                .MaximumLength(50); 
        }
    }
}
