using Application.Bases;

namespace Application.Features.Auth.Exceptions
{
    public class UserNameExistException : BaseExceptions
    {
        public UserNameExistException() : base("İstifadəçi adı mövcuddur.")
        {
        }
    }
}
