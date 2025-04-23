using Sport_Web.DTO;
using Sport_Web.Enums;
using Sport_Web.Models;

namespace Sport_Web.Abstraction
{
	public interface ICartService
	{
		Task<CartResponseDto> GetCartByUserIdAsync(int userId);
		Task<CartResponseDto> AddToCartAsync(CartDto cartDto);
		Task<CartResponseDto> UpdateCartAsync(UpdateCartDto updateCartDto);
		Task<bool> RemoveFromCartAsync(int cartItemId);
		Task<bool> CheckoutAsync(int userId);


		Task<List<CartResponseDto>> GetAllCartsAsync();
		Task<DeliveryResponseDto> UpdateDeliveryStatusAsync(int cartId, DeliveryStatus status);
		Task<CartResponseDto> GetCartDetailsAsync(int cartId);

	}
}



