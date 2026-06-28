using Application.Bases;

namespace Application.Features.Auth.Exceptions
{
    public class UserEmailExistException : BaseExceptions
    {
        public UserEmailExistException() : base("Email mövcuddur.")
        {
        }
    }
}
