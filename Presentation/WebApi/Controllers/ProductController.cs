using Application.Features.Products.Command;
using Application.Features.Products.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll([FromBody]GetAllProductQueryRequest request)
        {
            var products = await _mediator.Send(request);
            return Ok(products);
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get([FromBody] GetByIdProductQueryRequest request)
        {
            var products = await _mediator.Send(request);
            return Ok(products);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Create([FromBody] CreateProductCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] UpdateProductCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteProductCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
