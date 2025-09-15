using Application.Bases;
using Application.Features.Auth.Register.Commands.Requests;
using Application.Features.Auth.Register.Rules;
using Application.Interfaces.AutoMappers;
using Application.Interfaces.UnitOfWorks;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Auth.Register.Commands
{
	public class RegisterCommandHandler : BaseHandler,IRequestHandler<RegisterCommandRequest, Unit>
	{
		private readonly AuthRules authRules;
		private readonly UserManager<User> userManager;
		private readonly RoleManager<Role> roleManager;

		public RegisterCommandHandler(AuthRules authRules, UserManager<User> userManager, RoleManager<Role> roleManager,IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
		{
			this.authRules = authRules;
			this.userManager = userManager;
			this.roleManager = roleManager;
		}
		public async Task<Unit> Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
		{
			var existUser = await userManager.FindByEmailAsync(request.Email);
			await authRules.UserShouldNotBeExist(existUser);

			User user = mapper.Map<RegisterCommandRequest, User>(request);
			user.UserName = request.Email;
			user.SecurityStamp = Guid.NewGuid().ToString();

			IdentityResult result = await userManager.CreateAsync(user, request.Password);
			if (result.Succeeded)
			{
				if(!await roleManager.RoleExistsAsync("user"))
				{
					await roleManager.CreateAsync(new Role()
					{
						Id = Guid.NewGuid(),
						Name = "user",
						NormalizedName = " USER",
						ConcurrencyStamp = Guid.NewGuid().ToString()
					});
				}

				await userManager.AddToRoleAsync(user, "user");
			}

			return Unit.Value;
		}
	}
}
