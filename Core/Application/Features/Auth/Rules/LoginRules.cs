using Application.Bases;
using Application.Features.Auth.Exceptions;
using Domain.Entities;

namespace Application.Features.Auth.Rules
{
	public class LoginRules : BaseRule
	{
		public Task UserOrPasswordShouldNotBeInvalid(User user, bool checkPasswordMatching)
		{
			if (user is null || !checkPasswordMatching)
				throw new UserOrPasswordShouldNotBeInvalidException();

			return Task.CompletedTask;
		}
	}
}
