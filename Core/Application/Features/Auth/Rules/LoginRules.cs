using Application.Bases;
using Application.Features.Auth.Exceptions;
using Domain.Entities;

namespace Application.Features.Auth.Rules
{
    public class LoginRules : BaseRules
    {
        public User UserMustBeExist(string username,  IList<User> users)
        {
            User? user =  users.SingleOrDefault(u => u.UserName.ToLower() == username.ToLower());

            if (user is null)
                throw new UserNotExistException();
            return user;
        } 
    }
}
