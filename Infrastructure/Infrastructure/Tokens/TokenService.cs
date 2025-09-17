using Application.Interfaces.Tokens;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Tokens
{
	public class TokenService : ITokenService
	{
		private readonly UserManager<User> _userManager;
		private readonly TokenSettings settings;
		public TokenService(IOptions<TokenSettings> options, UserManager<User> userManager)
		{
			settings = options.Value;
			_userManager = userManager;
		}
		public async Task<JwtSecurityToken> CreateToken(User user, IList<string> roles)
		{
			var claims = new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(ClaimTypes.Name, user.Id.ToString()),
				new Claim(JwtRegisteredClaimNames.Email, user.Email),
			};

			foreach (var role in roles)
			{
				claims.Add(new Claim(ClaimTypes.Role, role));
			}

			SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Secret));
			SigningCredentials credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				issuer: settings.Issuer,
				audience: settings.Audience,
				claims: claims,
				expires: DateTime.Now.AddMinutes(settings.TokenValidityInMinutes),
				signingCredentials: credential
			);


			await _userManager.AddClaimsAsync(user, claims);
			return token;
		}

		public string GenerateRefreshToken()
		{
			var randomNumbers = new byte[64];
			var rng = RandomNumberGenerator.Create();
			rng.GetBytes(randomNumbers);

			return Convert.ToBase64String(randomNumbers);
		}

		public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
		{
			TokenValidationParameters parameters = new()
			{
				ValidateIssuer = false,
				ValidateAudience = false,
				ValidateIssuerSigningKey = true,
				ValidateLifetime = false,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Secret)),
			};

			var handler = new JwtSecurityTokenHandler();
			var principal = handler.ValidateToken(token, parameters, out SecurityToken securityToken);

			if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
				throw new SecurityTokenException();

			return principal;
		}
	}
}
