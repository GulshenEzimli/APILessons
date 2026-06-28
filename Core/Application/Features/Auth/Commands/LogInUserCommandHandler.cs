using Application.Features.Auth.Exceptions;
using Application.Interfaces.Tokens;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace Application.Features.Auth.Commands
{
    public class LogInUserCommandHandler : IRequestHandler<LogInUserCommandRequest, LogInUserCommandResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenservice;
        private readonly IConfiguration _configuration;

        public LogInUserCommandHandler(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenservice, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenservice = tokenservice;
            _configuration = configuration;
        }


        public async Task<LogInUserCommandResponse> Handle(LogInUserCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user is null)
                throw new UserNotExistException();

            var result = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!result)
                throw new InCorrectPasswordException();

            IList<string> roles = await _userManager.GetRolesAsync(user);
            var (token, expiresAt) = await _tokenservice.CreateToken(user, roles);
            var _token = new JwtSecurityTokenHandler().WriteToken(token);

            var refreshToken = _tokenservice.GenerateRefreshToken();
            _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryDate = DateTime.Now.AddDays(refreshTokenValidityInDays);

            await _userManager.UpdateAsync(user);
            await _userManager.UpdateSecurityStampAsync(user);
            await _userManager.SetAuthenticationTokenAsync(user, "Default", "Access", _token);

            return new LogInUserCommandResponse
            {
                IsSuccess = true,
                Token = _token,
                RefreshToken = refreshToken,
                ExpiresAt = expiresAt
            };
        }
    }
}
