using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sport_Web.Abstraction;
using Sport_Web.Data;
using Sport_Web.DTO;
using Sport_Web.Enums;
using Sport_Web.Models;

namespace Sport_Web.Implementation
{

	public class CartService : ICartService
	{
		private readonly ApplicationDbContext _context;

		public CartService(ApplicationDbContext context)
		{
			_context = context;
		}


		public async Task<CartResponseDto> GetCartByUserIdAsync(int userId)
		{
			var cart = await _context.Carts
				.Where(c => c.UserId == userId)
				.Include(c => c.Items)
				.ThenInclude(ci => ci.Product)
				.FirstOrDefaultAsync();

			if (cart == null)
			{
				return new CartResponseDto
				{
					IsSuccess = false,
					Message = "Cart not found."
				};
			}

			return new CartResponseDto
			{
				IsSuccess = true,
				Message = "Cart retrieved successfully.",
				CartId = cart.Id,
				UserId = cart.UserId,				
				Items = cart.Items.Select(ci => new CartItemDto
				{
					ProductId = ci.ProductId,
					ProductName = ci.Product.Name,
					Quantity = ci.Quantity,
					Price = ci.Product.Price,
					TotalPrice = ci.Quantity * ci.Product.Price
				}).ToList(),
				TotalAmount = cart.Items.Sum(ci => ci.Quantity * ci.Product.Price)
			};
		}

		public async Task<CartResponseDto> AddToCartAsync(CartDto addToCartDto)
		{
			var userCart = await _context.Carts
				.Where(c => c.UserId == addToCartDto.UserId)
				.FirstOrDefaultAsync();

			// Check if the user is in cooldown period
			if (userCart != null && userCart.LastCheckoutTime.HasValue)
			{
				var cooldownTimeSpan = DateTime.UtcNow - userCart.LastCheckoutTime.Value;
				var cooldownPeriod = TimeSpan.FromMinutes(30); // Example cooldown period (30 minutes)

				if (cooldownTimeSpan < cooldownPeriod)
				{
					return new CartResponseDto
					{
						IsSuccess = false,
						Message = "You cannot add items to the cart right now. Please wait for the cooldown period to end."
					};
				}
			}

			// Proceed with adding items to the cart as normal
			var product = await _context.Products.FindAsync(addToCartDto.ProductId);

			if (product == null)
				return new CartResponseDto { IsSuccess = false, Message = "Product not found." };

			var cart = userCart ?? new Cart { UserId = addToCartDto.UserId };

			if (cart.Id == 0)
			{
				_context.Carts.Add(cart);
				await _context.SaveChangesAsync();
			}

			var cartItem = await _context.CartItems
				.FirstOrDefaultAsync(ci => ci.CartId == cart.Id && ci.ProductId == addToCartDto.ProductId);

			if (cartItem == null)
			{
				_context.CartItems.Add(new CartItem
				{
					CartId = cart.Id,
					ProductId = addToCartDto.ProductId,
					Quantity = addToCartDto.Quantity
				});
			}
			else
			{
				cartItem.Quantity += addToCartDto.Quantity;  // Increase quantity if item already exists in the cart
			}

			await _context.SaveChangesAsync();

			return new CartResponseDto
			{
				IsSuccess = true,
				Message = "Product added to cart.",
				CartId = cart.Id,
				UserId = cart.UserId,
				Items = cart.Items.Select(ci => new CartItemDto
				{
					ProductId = ci.ProductId,
					ProductName = ci.Product.Name,
					Quantity = ci.Quantity,
					Price = ci.Product.Price,
					TotalPrice = ci.Quantity * ci.Product.Price
				}).ToList(),
				TotalAmount = cart.Items.Sum(ci => ci.Quantity * ci.Product.Price)
			};
		}


		public async Task<bool> RemoveFromCartAsync(int cartItemId)
		{

			var cartItem = await _context.CartItems.FindAsync(cartItemId);

			if (cartItem == null)
			{
				return false; // Item not found
			}

			_context.CartItems.Remove(cartItem);
			await _context.SaveChangesAsync();

			return true; // Successfully removed
		}



		public async Task<CartResponseDto> UpdateCartAsync(int userId, int productId, int newQuantity)
		{
			var cart = await _context.Carts
				.Where(c => c.UserId == userId)
				.Include(c => c.Items)
					.ThenInclude(ci => ci.Product)  // Ensure Product is included
				.FirstOrDefaultAsync();

			if (cart == null)
				return new CartResponseDto { IsSuccess = false, Message = "Cart not found." };

			var cartItem = await _context.CartItems
				.FirstOrDefaultAsync(ci => ci.CartId == cart.Id && ci.ProductId == productId);

			if (cartItem == null)
				return new CartResponseDto { IsSuccess = false, Message = "Product not found in cart." };

			cartItem.Quantity = newQuantity;
			await _context.SaveChangesAsync();

			return new CartResponseDto
			{
				IsSuccess = true,
				Message = "Cart updated successfully.",
				CartId = cart.Id,
				UserId = cart.UserId,
				Items = cart.Items.Select(ci => new CartItemDto
				{
					ProductId = ci.ProductId,
					ProductName = ci.Product.Name,
					Quantity = ci.Quantity,
					Price = ci.Product.Price,
					TotalPrice = ci.Quantity * ci.Product.Price
				}).ToList(),
				TotalAmount = cart.Items.Sum(ci => ci.Quantity * ci.Product.Price)
			};
		}


		public async Task<bool> CheckoutAsync(int userId)
		{
			// Fetch the cart for the user
			var cart = await _context.Carts
				.Where(c => c.UserId == userId && c.IsActive)
				.Include(c => c.Items)
				.FirstOrDefaultAsync();

			if (cart == null) return false;

			// Create or update delivery info
			if (cart.Delivery == null)
			{
				cart.Delivery = new Delivery
				{
					CartId = cart.Id,
					Status = DeliveryStatus.Received, // Set default status as Received
					DeliveryDate = DateTime.UtcNow.AddDays(5) // Default delivery date
				};
				_context.Deliveries.Add(cart.Delivery);
			}
			else
			{
				cart.Delivery.Status = DeliveryStatus.Completed; // If delivery exists, update status
			}

			// Deactivate the cart after checkout
			cart.IsActive = false;

			// Set the LastCheckoutTime
			cart.LastCheckoutTime = DateTime.UtcNow;

			// Save changes to the database
			await _context.SaveChangesAsync();

			return true; // Checkout was successful
		}

		public async Task<List<CartResponseDto>> GetAllCartsAsync()
		{
			return await _context.Carts
								 .Include(c => c.Items)
								  .Select(c=> new CartResponseDto
								  {


									  IsSuccess = true,
								       Message = "Cart updated successfully.",
									  CartId = c.Id,
									  UserId = c.UserId,
									  Items = c.Items.Select(ci => new CartItemDto
									  {
										  ProductId = ci.ProductId,
										  ProductName = ci.Product.Name,
										  Quantity = ci.Quantity,
										  Price = ci.Product.Price,
										  TotalPrice = ci.Quantity * ci.Product.Price
									  }).ToList(),
									  TotalAmount = c.Items.Sum(ci => ci.Quantity * ci.Product.Price)

								  })// Assuming your Cart has a related Items collection
								 .ToListAsync();
		}


		public async Task<DeliveryResponseDto> UpdateDeliveryStatusAsync(int cartId, DeliveryStatus status)
		{
			var delivery = await _context.Deliveries.FirstOrDefaultAsync(c => c.CartId == cartId);
			if (delivery == null)
			{
				return null;
			}

			delivery.Status = status;

			if (status == DeliveryStatus.Completed)
			{
				delivery.DeliveryDate = DateTime.UtcNow;
			}

			await _context.SaveChangesAsync();

			return new DeliveryResponseDto
			{
				DeliveryId = delivery.Id,
				CartId = delivery.CartId,
				Status = delivery.Status,
				DeliveryDate = delivery.DeliveryDate
			};
		}


		public async Task<CartResponseDto> GetCartDetailsAsync(int cartId)
		{
		 var cartDetail =  await _context.Carts
								 .Include(c => c.Items)
								 .ThenInclude(i => i.Product) // Make sure to include Product to avoid null

								 .FirstOrDefaultAsync(c => c.Id == cartId);
			if(cartDetail == null)
			{
				var response = new ResponseDto
				{
					IsSuccess = false,
					Message = "Invalid CartId"
				};
			}

			return new CartResponseDto
			{			
				Id = cartId,
				UserId = cartDetail.UserId,
				Items = cartDetail.Items.Select(ci => new CartItemDto
				{
					ProductId = ci.ProductId,
					ProductName = ci.Product.Name,
					Quantity = ci.Quantity,
					Price = ci.Product.Price,
					TotalPrice = ci.Quantity * ci.Product.Price
				}).ToList(),
				TotalAmount = cartDetail.Items.Sum(ci => ci.Quantity * ci.Product.Price)
			};

		}

	}
}
