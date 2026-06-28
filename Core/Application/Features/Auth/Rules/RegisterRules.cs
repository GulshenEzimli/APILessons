using Application.Bases;
using Application.Features.Auth.Exceptions;
using Domain.Entities;

namespace Application.Features.Auth.Rules
{
    public class RegisterRules : BaseRules
    {
        public void UserNameCannotBeExist(string username, IList<User> users)
        {
            bool isExist = users.Any(u => u.UserName.ToLower() == username.ToLower());

            if(isExist)
                throw new UserNameExistException();
        }

        public void EmailCannotBeExist(string email, IList<User> users)
        {
            bool isExist = users.Any(u => u.Email.ToLower() == email.ToLower());

            if (isExist)
                throw new UserEmailExistException();
        }

        public void PhoneNumberCannotBeExist(string phone, IList<User> users)
        {
            bool isExist = users.Any(u => u.PhoneNumber.ToLower() == phone.ToLower());

            if (isExist)
                throw new UserPhoneNumberExistException();
        }
    }
}
