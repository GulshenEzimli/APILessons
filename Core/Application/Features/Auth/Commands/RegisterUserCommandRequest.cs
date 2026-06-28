using MediatR;

namespace Application.Features.Auth.Commands
{
    public class RegisterUserCommandRequest : IRequest<RegisterUserCommandResponse>
    {
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public List<string> Roles { get; set; }
    }
}
