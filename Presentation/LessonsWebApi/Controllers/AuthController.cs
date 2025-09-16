using Application.Features.Auth.Register.Commands.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LessonsWebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IMediator mediator;

		public AuthController(IMediator mediator)
		{
			this.mediator = mediator;
		}

		[HttpPost]
		public async Task<IActionResult> Register([FromForm] RegisterCommandRequest request)
		{
			var result = await mediator.Send(request);
			return Ok(result);
		}
	}
}
