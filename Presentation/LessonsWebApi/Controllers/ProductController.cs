using Application.Interfaces.UnitOfWorks;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LessonsWebApi.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;
		public ProductController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var entities = await _unitOfWork.GetReadRepository<Product>().GetAllAsync();
			return Ok(entities);
		}

	}
}
