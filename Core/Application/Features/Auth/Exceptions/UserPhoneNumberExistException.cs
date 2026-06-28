using Application.Bases;

namespace Application.Features.Auth.Exceptions
{
    public class UserPhoneNumberExistException : BaseExceptions
    {
        public UserPhoneNumberExistException() : base("Bu telefon nömrəsi artıq mövcuddur.")
        {
        }
    }
}
