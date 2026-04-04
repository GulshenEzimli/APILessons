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
        public async Task<JwtSecurityToken> CreateToken(User user, IList<string> roles)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),   
                new Claim(ClaimTypes.Email, user.Email),
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var secretkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.Secret));
            var signinCredential = new SigningCredentials(secretkey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer : _tokenSettings.Issuer,
                audience : _tokenSettings.Audience,
                claims : claims,
                expires : DateTime.Now.AddMinutes(_tokenSettings.TokenValidityInMinutes),
                signingCredentials : signinCredential
                );

            await _userManager.AddClaimsAsync(user, claims);

            return token;
        }

        public string GenerateRefreshToken()
        {
            byte[] randomNumbers = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumbers);

            return Convert.ToBase64String(randomNumbers);
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string? token)
        {
            TokenValidationParameters validator = new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.Secret)),
                ValidateLifetime = false,
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            ClaimsPrincipal claims = handler.ValidateToken(token, validator, out SecurityToken securityToken);

            if(securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg
                .Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Token tapilmadi");
            }

            return claims;
        }
    }
}
