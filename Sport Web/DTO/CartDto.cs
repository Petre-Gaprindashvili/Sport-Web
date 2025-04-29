using Sport_Web.Enums;
using Sport_Web.Models;

namespace Sport_Web.DTO
{
	public class CartDto
	{
		public int UserId { get; set; }
		public int ProductId { get; set; }
		public int Quantity { get; set; }
	}

	public class UpdateCartDto
	{
		public int UserId { get; set; }
		public int ProductId { get; set; }
		public int NewQuantity { get; set; }
	}



	public class CartResponseDto
	{
		public int Id { get; set; }	

		public int CartId { get; set; }
		public int UserId { get; set; }
		public bool IsActive { get; set; }	
		public List<CartItemDto> Items { get; set; }
		public decimal TotalAmount { get; set; }
	}

	public class CartItemDto
	{
		public int ProductId { get; set; }
		public string imgUrl { get; set; }	
		public string ProductName { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
		public decimal TotalPrice { get; set; }
		public int Stock {  get; set; }	
	}

	public class DeliveryResponseDto
	{
		public int DeliveryId { get; set; }
		public int CartId { get; set; }
		public DeliveryStatus Status { get; set; }
		public DateTime? DeliveryDate { get; set; }
	}
}

