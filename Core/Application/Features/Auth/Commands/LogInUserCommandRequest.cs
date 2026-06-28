using MediatR;

namespace Application.Features.Auth.Commands
{
    public class LogInUserCommandRequest : IRequest<LogInUserCommandResponse>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
