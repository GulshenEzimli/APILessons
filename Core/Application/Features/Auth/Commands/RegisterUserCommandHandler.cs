using Application.Features.Auth.Exceptions;
using Application.Interfaces.AutoMapper;
using Application.Interfaces.Tokens;
using Application.Interfaces.UnitOfWorks;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace Application.Features.Auth.Commands
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommandRequest, RegisterUserCommandResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly ITokenService _jwtTokenService;
        private readonly ICustomMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public RegisterUserCommandHandler(ICustomMapper mapper, IUnitOfWork unitOfWork, UserManager<User> userManager, ITokenService jwtTokenService, IHttpContextAccessor httpContextAccessor, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _roleManager = roleManager;
        }
        public async Task<RegisterUserCommandResponse> Handle(RegisterUserCommandRequest request, CancellationToken cancellationToken)
        {
            var existingByName = await _userManager.FindByNameAsync(request.Username);
            if (existingByName != null)
                throw new UserNameExistException();

            var existingByEmail = await _userManager.FindByEmailAsync(request.Email);
            if (existingByEmail != null)
                throw new UserEmailExistException();

            var user = _mapper.Map<RegisterUserCommandRequest, User>(request);
            user.SecurityStamp = Guid.NewGuid().ToString();

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                return new RegisterUserCommandResponse
                {
                    IsSuccess = false,
                    Message = string.Join('|', result.Errors.Select(e => e.Description))
                };

            foreach (var role in request.Roles)
            {
                bool roleExist = await _roleManager.RoleExistsAsync(role);
                if (!roleExist)
                {
                    await _roleManager.CreateAsync(new Role
                    {
                        Id = Guid.NewGuid(),
                        Name = role,
                        NormalizedName = role.ToUpper(),
                        ConcurrencyStamp = Guid.NewGuid().ToString()
                    });
                }

                await _userManager.AddToRoleAsync(user, role);
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var (token, expiresAt) = await _jwtTokenService.CreateToken(user, userRoles);

            return new RegisterUserCommandResponse
            {
                IsSuccess = true,
                Message = "Qeydiyyat uğurla tamamlandı.",
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                TokenExpiresAt = expiresAt
            };
        }
    }
}
