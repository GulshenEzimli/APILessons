using Application.Bases;

namespace Application.Features.Auth.Exceptions
{
	public class UserOrPasswordShouldNotBeInvalidException : BaseException
	{
		public UserOrPasswordShouldNotBeInvalidException() : base("İstifadəçi adı ve ya şifrə səhvdir.")
		{
		}
	}
}
