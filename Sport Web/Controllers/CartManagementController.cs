using Microsoft.AspNetCore.Mvc;
using Sport_Web.Abstraction;
using Sport_Web.Enums;

namespace Sport_Web.Controllers
{

	public class CartManagementController :ControllerBase
	{

		private readonly ICartService _cartService;
		public CartManagementController(ICartService cartService)
		{
			_cartService = cartService;
		}

		[HttpGet("kkkkkk")]
		public async Task<IActionResult> GetAllCarts()
		{
			var carts = await _cartService.GetAllCartsAsync();
			return Ok(carts);
		}

		[HttpPut("{cartId}/delivery-status")]
		public async Task<IActionResult> UpdateDeliveryStatus(int cartId,  DeliveryStatus status)
		{
			var result = await _cartService.UpdateDeliveryStatusAsync(cartId, status);
			return Ok(result);	

		}

		[HttpGet("{cartId}")]
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
