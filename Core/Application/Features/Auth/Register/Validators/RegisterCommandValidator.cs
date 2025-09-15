using Application.Features.Auth.Register.Commands.Requests;
using FluentValidation;

namespace Application.Features.Auth.Register.Validators
{
	public class RegisterCommandValidator : AbstractValidator<RegisterCommandRequest>
	{
		public RegisterCommandValidator()
		{
			RuleFor(r => r.FullName).NotEmpty().MinimumLength(2).MaximumLength(20).WithName("Ad Soyad");
			RuleFor(r => r.Email).NotEmpty().MinimumLength(11).MaximumLength(60).EmailAddress().WithName("E-mail Adresi");
			RuleFor(r => r.Password).NotEmpty().MinimumLength(6).WithName("Parol");
			RuleFor(r => r.ConfirmPassword).NotEmpty().MinimumLength(6).Equal(x => x.Password).WithName("Parol təkrarı").WithMessage("Parol təkrarı uyğunlaşmır.");

		}
	}
}
