using Application.Bases;
using Application.Features.Auth.Rules;
using Application.Interfaces.AutoMappers;
using Application.Interfaces.Tokens;
using Application.Interfaces.UnitOfWorks;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace Application.Features.Auth.Login.Commands
{
	public class LoginCommandHandler : BaseHandler, IRequestHandler<LoginCommandRequest, LoginCommandResponse>
	{
		private readonly LoginRules _loginRules;
		private readonly UserManager<User> _userManager;
		private readonly ITokenService _tokenService;
		private readonly IConfiguration configuration;
		public LoginCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, LoginRules loginRules, UserManager<User> userManager, ITokenService tokenService, IConfiguration configuration) : base(mapper, unitOfWork, httpContextAccessor)
		{
			_loginRules = loginRules;
			_userManager = userManager;
			_tokenService = tokenService;
			this.configuration = configuration;
		}
		public async Task<LoginCommandResponse> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
		{
			User user = await _userManager.FindByEmailAsync(request.Email);
			bool checkPassword = await _userManager.CheckPasswordAsync(user, request.Password);

			await _loginRules.UserOrPasswordShouldNotBeInvalid(user, checkPassword);

			IList<string> roles = await _userManager.GetRolesAsync(user);

			JwtSecurityToken token = await _tokenService.CreateToken(user, roles);
			string refreshToken = _tokenService.GenerateRefreshToken();

			_ = int.TryParse(configuration["JWT : RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

			user.RefreshToken = refreshToken;
			user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);

			await _userManager.UpdateAsync(user);
			await _userManager.UpdateSecurityStampAsync(user);

			string _token = new JwtSecurityTokenHandler().WriteToken(token);
			 await _userManager.SetAuthenticationTokenAsync(user, "Default", "AccessToken", _token);

			return new LoginCommandResponse()
			{
				Token = _token,
				RefreshToken = refreshToken,
				Expiration = token.ValidTo,
			};
			
		}
	}
}
