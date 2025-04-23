using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sport_Web.Abstraction;
using Sport_Web.Enums;

namespace Sport_Web.Controllers
{
	[ApiController]
	[Route("api/admin/carts")]
	[Authorize(Roles = "Admin")]

	public class CartManagementController :ControllerBase
	{

		private readonly ICartService _cartService;
		public CartManagementController(ICartService cartService)
		{
			_cartService = cartService;
		}

		[HttpGet("GetAllCart")]
		public async Task<IActionResult> GetAllCarts()
		{
			var carts = await _cartService.GetAllCartsAsync();
			return Ok(carts);
		}

		[HttpPut("Delivery-status/{cartId}")]
		public async Task<IActionResult> UpdateDeliveryStatus(int cartId,  DeliveryStatus status)
		{
			var result = await _cartService.UpdateDeliveryStatusAsync(cartId, status);
			return Ok(result);	

		}

		[HttpGet("GetCartDetails{cartId}")]
		public async Task<IActionResult> GetCartDetails(int cartId)
		{
			var cart = await _cartService.GetCartDetailsAsync(cartId);
			if (cart == null)
			{
				return NotFound();
			}
			return Ok(cart);
		}

	}
}
