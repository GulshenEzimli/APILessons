namespace Application.Features.Auth.Commands
{
    public class RegisterUserCommandResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string AccessToken { get; set; }
        public DateTime TokenExpiresAt { get; set; }
    }
}
