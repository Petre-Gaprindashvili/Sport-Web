using Microsoft.AspNetCore.Mvc;
using Sport_Web.Abstraction;
using Sport_Web.DTO;
using Sport_Web.Implementation;

namespace Sport_Web.Controllers
{
	public class ProductController : ControllerBase
	{


		private readonly IProductService _productService;	

		public ProductController(IProductService productService)
		{
			_productService = productService;	
		}

		[HttpGet("GetProduct/{teamId}")]
		public async Task<ActionResult<List<PlayersResponseDto>>> GetProductByTeam(int teamId)
		{
			var products = await _productService.GetProductsByTeamIdAsync(teamId);
			return Ok(products);
		}
	}
}


