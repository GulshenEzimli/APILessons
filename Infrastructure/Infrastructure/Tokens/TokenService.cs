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
        private readonly TokenSettings _tokenSettings;
        private readonly UserManager<User> _userManager;
        public TokenService(IOptions<TokenSettings> options, UserManager<User> userManager)
        {
            _tokenSettings = options.Value;
            _userManager = userManager;
        }
        public async Task<(JwtSecurityToken, DateTime)> CreateToken(User user, IList<string> roles)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName ?? string.Empty),   
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
                new Claim("Fullname", user.FullName),
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var secretkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.Secret));
            var signinCredential = new SigningCredentials(secretkey, SecurityAlgorithms.HmacSha256);
            var expiresAt = DateTime.Now.AddMinutes(_tokenSettings.TokenValidityInMinutes);

            var token = new JwtSecurityToken(
                issuer : _tokenSettings.Issuer,
                audience : _tokenSettings.Audience,
                claims : claims,
                expires : expiresAt,
                signingCredentials : signinCredential
                );

            await _userManager.AddClaimsAsync(user, claims);
            return (token, expiresAt);
        }

        public string GenerateRefreshToken()
        {
            var randomNumbers = new byte[64];
            using var rng = RandomNumberGenerator.Create(); 
            rng.GetBytes(randomNumbers);

            return Convert.ToBase64String(randomNumbers);   
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string? token)
        {
            TokenValidationParameters validationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.Secret)),
            };

            var handler = new JwtSecurityTokenHandler();
            ClaimsPrincipal claims = handler.ValidateToken(token, validationParameters, out SecurityToken securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Token yoxdur.");

            return claims;
        }
    }
}
