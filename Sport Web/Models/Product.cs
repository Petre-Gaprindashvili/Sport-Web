﻿namespace Sport_Web.Models
{
	public class Product
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
		public string Description { get; set; }	
		public string ImageUrl { get; set; }
		public int? Quantity { get; set; }	
		public int Stock { get; set; } 
		public bool IsAvailable { get; set; }
		public int? TeamId { get; set; }
		public Team Team { get; set; }
	}
}


