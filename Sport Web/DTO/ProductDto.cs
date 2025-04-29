namespace Sport_Web.DTO
{
	public class ProductDtocs
	{
		public string Name { get; set; }
		public decimal Price { get; set; }
		public string Description { get; set; }
		public string ImageUrl { get; set; }
		public int Stock { get; set; }
		public bool IsAvailable { get; set; }
		public int? TeamId { get; set; }
		public int Quantity { get; set; }

	}

	public class ProductResponseDtocs
	{
		public int Id { get; set; }	
		public string Name { get; set; }
		public decimal Price { get; set; }
		public string Description { get; set; }
		public string? ImageUrl { get; set; }
		public int Stock { get; set; }
		public bool IsAvailable { get; set; }
		public int? TeamId { get; set; }
		public int Quantity { get; set; } 

	}

}
