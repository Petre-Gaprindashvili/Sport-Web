using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sport_Web.Abstraction;
using Sport_Web.DTO;
using Sport_Web.Implementation;

namespace Sport_Web.Controllers
{
	[ApiController]
	[Route("api/admin/products")]
	[Authorize(Roles = "Admin")]

	public class ProductManagementController : ControllerBase
	{

		private readonly IProductService _productService;

		public ProductManagementController(IProductService productService)
		{
			_productService = productService;
		}

		[HttpPost("AddProduct")]
		public async Task<IActionResult> AddProduct([FromBody] ProductDtocs productDtocs)
		{
			var players = await _productService.AddProductAsync(productDtocs);
			return Ok(players);
		}

		[HttpGet("GetProduct{id}")]
		public async Task<ActionResult<PlayersResponseDto>> GetProductById(int id)
		{
			var product = await _productService.GetProductByIdAsync(id);
			return Ok(product);
		}

		[HttpPut("updateProduct/{id}")]
		public async Task<ActionResult<ProductResponseDtocs>> UpdateProduct(int id, ProductDtocs productDto)
		{
			var updatedProduct = await _productService.UpdateProductAsync(id, productDto);
			return Ok(updatedProduct);
		}

		[HttpDelete("deleteProduct/{id}")]
		public async Task<ActionResult> DeleteProduct(int id)
		{
			var result = await _productService.DeleteProductAsync(id);
			return Ok(result);
		}

	}
}
