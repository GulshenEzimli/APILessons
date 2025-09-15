using Application.Bases;
using Application.Features.Auth.Register.Exceptions;
using Domain.Entities;

namespace Application.Features.Auth.Register.Rules
{
	public class AuthRules: BaseRule
	{
		public Task UserShouldNotBeExist(User? user)
		{
			if (user is not null)
				throw new UserAlreadyExistException();

			return Task.CompletedTask;
		}
	}
}
