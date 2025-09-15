using Application.Bases;

namespace Application.Features.Auth.Register.Exceptions
{
	public class UserAlreadyExistException : BaseException
	{
		public UserAlreadyExistException() : base("Bu istifadəçi artıq mövcuddur.")
		{
		}
	}
}
