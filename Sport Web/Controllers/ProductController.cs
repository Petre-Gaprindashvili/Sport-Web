using Microsoft.AspNetCore.Mvc;
using Sport_Web.Abstraction;
using Sport_Web.DTO;
using Sport_Web.Implementation;

namespace Sport_Web.Controllers
{
	[ApiController]
	[Route("api/products")]
	public class ProductController : ControllerBase
	{


		private readonly IProductService _productService;	

		public ProductController(IProductService productService)
		{
			_productService = productService;	
		}

		[HttpGet("GetProductByTeam/{teamId}")]
		public async Task<ActionResult<List<ProductResponseDtocs>>> GetProductByTeam(int teamId)
		{
			var products = await _productService.GetProductsByTeamIdAsync(teamId);
			return Ok(products);
		}

		[HttpGet("GetProduct/{id}")]
		public async Task<ProductResponseDtocs> GetProductById(int id)
		{
			var product = await _productService.GetProductByIdAsync(id);
			return product;
	}


}
}


