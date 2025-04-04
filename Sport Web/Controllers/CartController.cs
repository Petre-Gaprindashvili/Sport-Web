using Microsoft.AspNetCore.Mvc;
using Sport_Web.Abstraction;
using Sport_Web.DTO;

namespace Sport_Web.Controllers
{
	[ApiController]
	[Route("api/cart")]
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
		public async Task<IActionResult> AddToCart(CartDto cartDto)
		{
			var cart = await _cartService.AddToCartAsync(cartDto);

			return Ok(cart);
		}

		[HttpPut("updateCart")]
		public async Task<IActionResult> UpdateCart( int userId,  int productId,  int newQuantity)
		{
			var updatedCart = await _cartService.UpdateCartAsync(userId, productId, newQuantity);
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
