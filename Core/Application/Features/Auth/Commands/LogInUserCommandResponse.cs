namespace Application.Features.Auth.Commands
{
    public class LogInUserCommandResponse
    {
        public bool IsSuccess { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
