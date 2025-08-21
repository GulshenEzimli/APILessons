using Application.Features.Queries.Request;
using Application.Features.Queries.Response;
using Application.Interfaces.UnitOfWorks;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LessonsWebApi.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IMediator _mediator;
		public ProductController(IMediator mediator)
		{
			_mediator = mediator;
		}
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var entities = await _mediator.Send<IList<GetAllProductQueryResponse>>(new GetAllProductQueryRequest());
			return Ok(entities);
		}

	}
}
