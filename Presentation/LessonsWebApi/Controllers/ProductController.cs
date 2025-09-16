using Application.Features.Products.Commands.Requests;
using Application.Features.Products.Queries.Request;
using MediatR;
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
			var entities = await _mediator.Send(new GetAllProductQueryRequest());
			return Ok(entities);
		}

		[HttpPost]
		public async Task<IActionResult> Create(CreateProductCommandRequest request)
		{
			await _mediator.Send(request);
			return Ok();
		}

		[HttpPost]
		public async Task<IActionResult> Update(UpdateProductCommandRequest request)
		{
			await _mediator.Send(request);
			return Ok();
		}

		[HttpPost]
		public async Task<IActionResult> Delete(DeleteProductCommandRequest request)
		{
			await _mediator.Send(request);
			return Ok();
		}
	}
}
