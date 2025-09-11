using Application.Interfaces.Tokens;
using Infrastructure.Tokens;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure
{
	public static class Registration
	{
		public static void AddInfrastructure(this IServiceCollection services, IConfiguration config)
		{
			services.Configure<TokenSettings>(config.GetSection("JWT"));

			services.AddTransient<ITokenService, TokenService>();

			services.AddAuthentication(opt =>
			{
				opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
			{
				opt.SaveToken = true;
				opt.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidateIssuer = false,
					ValidateAudience = false,
					ValidateIssuerSigningKey = true,
					ValidateLifetime = false,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT : Secret"])),
					ValidIssuer = config["JWT : Issuer"],
					ValidAudience = config["JWT : Audience"],
					ClockSkew = TimeSpan.Zero
				};
			});

		}
	}
}
