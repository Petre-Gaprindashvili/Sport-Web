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
				.Where(c => c.UserId == userId && c.IsActive)
				.Include(c => c.Items)
				.ThenInclude(ci => ci.Product)
				.FirstOrDefaultAsync();

			if (cart == null)
			{
				return new CartResponseDto
				{
					Id = 0,
					CartId = 0,
					UserId = userId,
					IsActive = false,
					Items = new List<CartItemDto>(), // Empty cart items
					TotalAmount = 0,
					
					//Message = "Your cart is empty."
				};

				
			}

			return new CartResponseDto
			{
			
				Id = cart.Id,
				CartId = cart.Id,
				UserId = cart.UserId,	
				IsActive = cart.IsActive,	
				
				Items = cart.Items.Select(ci => new CartItemDto
				{
					ProductId = ci.ProductId,
					imgUrl = ci.Product.ImageUrl,
					ProductName = ci.Product.Name,
					Quantity = ci.Quantity,
					Price = ci.Product.Price,
					TotalPrice = ci.Quantity * ci.Product.Price,
					Stock = ci.Product.Stock,	
				}).ToList(),
				TotalAmount = cart.Items.Sum(ci => ci.Quantity * ci.Product.Price)
			};
		}

		public async Task<CartResponseDto> AddToCartAsync(CartDto addToCartDto)
		{
			// Check if the user already has an active cart
			var userCart = await _context.Carts
				.Where(c => c.UserId == addToCartDto.UserId && c.IsActive) // Get only active cart
				.Include(c => c.Items)
				.FirstOrDefaultAsync();

			// If no active cart exists, create a new one
			if (userCart == null)
			{
				userCart = new Cart
				{
					UserId = addToCartDto.UserId,
					IsActive = true,  // The new cart will be active
					LastCheckoutTime = null  // Reset LastCheckoutTime for the new cart
				};

				_context.Carts.Add(userCart);
				await _context.SaveChangesAsync(); // Save to generate CartId
			}

			// Check if the product exists in the database
			var product = await _context.Products.FindAsync(addToCartDto.ProductId);
			if (product == null)
			{
				throw new Exception("Product not found");
			}

			// Check if the product is already in the cart
			var cartItem = await _context.CartItems
				.FirstOrDefaultAsync(ci => ci.CartId == userCart.Id && ci.ProductId == addToCartDto.ProductId);

			// Calculate the total requested quantity (existing + new)
			int totalRequesedQuantity = addToCartDto.Quantity + (cartItem?.Quantity ?? 0);
			if (totalRequesedQuantity > product.Stock)
			{

				int availableToAdd = product.Stock - (cartItem?.Quantity ?? 0);
				throw new Exception($"Only {availableToAdd} item(s) available in stock for '{product.Name}'.");
			}

			if (cartItem == null)
			{
				// Add the product to the cart if it doesn't exist
				cartItem = new CartItem
				{
					CartId = userCart.Id,
					ProductId = addToCartDto.ProductId,
					Quantity = addToCartDto.Quantity
				};
				_context.CartItems.Add(cartItem);
			}
			else
			{
				// Update the quantity if the product is already in the cart
				cartItem.Quantity += addToCartDto.Quantity;
			}

			// Save changes to the database
			await _context.SaveChangesAsync();

			// Return the updated cart response
			return new CartResponseDto
			{
				Id = userCart.Id,
				CartId = userCart.Id,
				UserId = userCart.UserId,
				IsActive = userCart.IsActive,
				Items = await _context.CartItems
					.Where(ci => ci.CartId == userCart.Id)
					.Select(ci => new CartItemDto
					{
						ProductId = ci.ProductId,
						imgUrl = ci.Product.ImageUrl,
						ProductName = ci.Product.Name,
						Quantity = ci.Quantity,
						Price = ci.Product.Price,
						TotalPrice = ci.Quantity * ci.Product.Price
					}).ToListAsync(),
				TotalAmount = await _context.CartItems
					.Where(ci => ci.CartId == userCart.Id)
					.SumAsync(ci => ci.Quantity * ci.Product.Price)
			};
		}


		public async Task<bool> RemoveFromCartAsync(int productId)
		{

			var cartItem = await _context.CartItems.FirstOrDefaultAsync(c=>c.ProductId == productId);

			if (cartItem == null)
			{
				return false; // Item not found
			}

			_context.CartItems.Remove(cartItem);
			await _context.SaveChangesAsync();

			return true; // Successfully removed
		}



		public async Task<CartResponseDto> UpdateCartAsync(UpdateCartDto updateCartDto)
		{
			var cart = await _context.Carts
				.Where(c => c.UserId == updateCartDto.UserId)
				.FirstOrDefaultAsync();

			if (cart == null)
				throw new Exception("Cart not found");

			var cartItem = await _context.CartItems
				.FirstOrDefaultAsync(ci => ci.CartId == cart.Id && ci.ProductId == updateCartDto.ProductId);
			if (cartItem == null)
			{
				throw new Exception("Cart item not found");

				//// If the item is not found, create a new cart item
				//cartItem = new CartItem
				//{
				//	CartId = cart.Id,
				//	ProductId = updateCartDto.ProductId,
				//	Quantity = updateCartDto.NewQuantity
				//};
				//_context.CartItems.Add(cartItem);
			}
			else
			{
					cartItem.Quantity = updateCartDto.NewQuantity;

			}
			//else
			//{
			//	// If the item already exists, increment the quantity
			//	cartItem.Quantity += updateCartDto.NewQuantity;
			//}


			// Check if cart still has at least one item with quantity > 0
			cart.IsActive = await _context.CartItems
				.AnyAsync(ci => ci.CartId == cart.Id && ci.Quantity > 0);

			await _context.SaveChangesAsync();

			// Reload updated items with product info to avoid stale data
			var updatedItems = await _context.CartItems
				.Where(ci => ci.CartId == cart.Id)
				.Include(ci => ci.Product)
				.ToListAsync();

			return new CartResponseDto
			{
				Id = cart.Id,
				CartId = cart.Id,
				UserId = cart.UserId,
				IsActive = cart.IsActive,
				Items = updatedItems.Select(ci => new CartItemDto
				{
					ProductId = ci.ProductId,
					ProductName = ci.Product.Name,
					Quantity = ci.Quantity,
					Price = ci.Product.Price,
					TotalPrice = ci.Quantity * ci.Product.Price
				}).ToList(),
				TotalAmount = updatedItems.Sum(ci => ci.Quantity * ci.Product.Price)
			};
		}



		public async Task<bool> CheckoutAsync(int userId)
		{
			// Fetch the latest active cart for the user
			var cart = await _context.Carts
				.Where(c => c.UserId == userId && c.IsActive)
				.Include(c => c.Items)
				.Include(c => c.Delivery)
				.FirstOrDefaultAsync();

			// If no active cart exists, return false (User has nothing to checkout)
			if (cart == null) return false;

			// Mark the current cart as inactive
			cart.IsActive = false;
			cart.LastCheckoutTime = DateTime.UtcNow;

			// Create or update delivery info
			if (cart.Delivery == null)
			{
				cart.Delivery = new Delivery
				{
					CartId = cart.Id,
					Status = DeliveryStatus.Received,
					DeliveryDate = DateTime.UtcNow.AddDays(5)
				};
				_context.Deliveries.Add(cart.Delivery);
			}
			else
			{
				cart.Delivery.Status = DeliveryStatus.Completed;
				_context.Entry(cart.Delivery).State = EntityState.Modified;
			}

			// Save checkout changes
			await _context.SaveChangesAsync();

			return true;
		}


		//public async Task<bool> CheckoutAsync(int userId)
		//{
		//	// Fetch the active cart for the user
		//	var cart = await _context.Carts
		//		.Where(c => c.UserId == userId && c.IsActive)
		//		.Include(c => c.Items)
		//		.Include(c => c.Delivery) // Include delivery to check if it exists
		//		.FirstOrDefaultAsync();

		//	if (cart == null) return false; // No active cart found

		//	// Create or update delivery info
		//	if (cart.Delivery == null)
		//	{
		//		cart.Delivery = new Delivery
		//		{
		//			CartId = cart.Id,
		//			Status = DeliveryStatus.Received,
		//			DeliveryDate = DateTime.UtcNow.AddDays(5) // Default delivery date
		//		};
		//		_context.Deliveries.Add(cart.Delivery);
		//	}
		//	else
		//	{
		//		cart.Delivery.Status = DeliveryStatus.Completed;
		//		_context.Entry(cart.Delivery).State = EntityState.Modified; // Ensure update
		//	}

		//	// Mark cart as inactive after checkout
		//	cart.IsActive = false;
		//	cart.LastCheckoutTime = DateTime.UtcNow;

		//	// Create a new active cart for the user after checkout
		//	//var newCart = new Cart
		//	//{
		//	//	UserId = userId,
		//	//	IsActive = true
		//	//};
		//	//_context.Carts.Add(newCart); // Add a new active cart for future items

		//	// Save changes to the database
		//	await _context.SaveChangesAsync();

		//	return true; // Checkout successful
		//}


		//public async Task<bool> CheckoutAsync(int userId)
		//{
		//	// Fetch the cart for the user
		//	var cart = await _context.Carts
		//		.Where(c => c.UserId == userId && c.IsActive)
		//		.Include(c => c.Items)
		//		.FirstOrDefaultAsync();

		//	if (cart == null) return false;

		//	// Create or update delivery info
		//	if (cart.Delivery == null)
		//	{
		//		cart.Delivery = new Delivery
		//		{
		//			CartId = cart.Id,
		//			Status = DeliveryStatus.Received, // Set default status as Received
		//			DeliveryDate = DateTime.UtcNow.AddDays(5) // Default delivery date
		//		};
		//		_context.Deliveries.Add(cart.Delivery);
		//	}
		//	else
		//	{
		//		cart.Delivery.Status = DeliveryStatus.Completed; // If delivery exists, update status
		//	}

		//	// Deactivate the cart after checkout
		//	cart.IsActive = false;

		//	// Set the LastCheckoutTime
		//	cart.LastCheckoutTime = DateTime.UtcNow;

		//	// Save changes to the database
		//	await _context.SaveChangesAsync();

		//	return true; // Checkout was successful
		//}

		public async Task<List<CartResponseDto>> GetAllCartsAsync()
		{
			return await _context.Carts
								 .Include(c => c.Items)
								  .Select(c=> new CartResponseDto
								  {

									  Id = c.Id,
									  CartId = c.Id,
									  UserId = c.UserId,
									  IsActive = c.IsActive,		
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
				Id = cartDetail.Id,	
				CartId = cartDetail.Id,	
				UserId = cartDetail.UserId,
				IsActive = cartDetail.IsActive,	
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
