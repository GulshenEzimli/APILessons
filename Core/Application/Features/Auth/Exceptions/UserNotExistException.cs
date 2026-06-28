using Application.Bases;

namespace Application.Features.Auth.Exceptions
{
    public class UserNotExistException : BaseExceptions
    {
        public UserNotExistException() : base("Giriş uğursuzdur.")
        {
        }
    }
}
