using Application.Interfaces.AutoMappers;
using Application.Interfaces.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Application.Bases
{
	public class BaseHandler
	{
		public readonly IMapper mapper;
		public readonly IUnitOfWork unitOfWork;
		public readonly IHttpContextAccessor httpContextAccessor;
		public string? userId;

		public BaseHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
		{
			this.mapper = mapper;
			this.unitOfWork = unitOfWork;
			this.httpContextAccessor = httpContextAccessor;
			userId = httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
		}
	}
}
