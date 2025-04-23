using Microsoft.AspNetCore.Mvc;
using Sport_Web.Abstraction;
using Sport_Web.DTO;

namespace Sport_Web.Controllers
{
	[ApiController]
	[Route("api/carts")]
	public class CartController : Controller
	{
		private readonly ICartService _cartService;	
		public CartController(ICartService cartService)
		{
			_cartService = cartService;
		}

		[HttpGet("GetCart{userId}")]
		public async Task<IActionResult> GetCartByUserId(int userId)
		{
			var cart = await _cartService.GetCartByUserIdAsync(userId);
			return Ok(cart);
		}

		[HttpPost("addTocart")]
		public async Task<IActionResult> AddToCart([FromBody] CartDto cartDto)
		{
			var cart = await _cartService.AddToCartAsync(cartDto);

			return Ok(cart);
		}

		[HttpPut("updateCart")]
		public async Task<IActionResult> UpdateCart( UpdateCartDto updateCartDto)
		{
			var updatedCart = await _cartService.UpdateCartAsync(updateCartDto);
			return Ok(updatedCart);	
		}


		[HttpDelete("RemoveFromCart/{cartItemId}")]
		public async Task<IActionResult> AddToCart(int cartItemId)
		{
			var cart = await _cartService.RemoveFromCartAsync(cartItemId);

			return Ok(cart);
		}

		[HttpPost("checkout/{userId}")]
		public async Task<IActionResult> Checkout(int userId)
		{
			var success = await _cartService.CheckoutAsync(userId);
			if (!success) return BadRequest("Checkout failed.");
			return Ok("Checkout successful.");
		}
	}
}
