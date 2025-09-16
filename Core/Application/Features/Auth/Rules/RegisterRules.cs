using Application.Bases;
using Application.Features.Auth.Exceptions;
using Domain.Entities;

namespace Application.Features.Auth.Rules
{
	public class RegisterRules: BaseRule
	{
		public Task UserShouldNotBeExist(User? user)
		{
			if (user is not null)
				throw new UserAlreadyExistException();

			return Task.CompletedTask;
		}
	}
}
