namespace Sport_Web.Models
{
	public class Cart
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public User User { get; set; }
		public ICollection<CartItem> Items { get; set; } = new List<CartItem>();
		public bool IsActive { get; set; } = true;	
		public Delivery Delivery { get; set; }
		public DateTime? LastCheckoutTime { get; set; }  // New field for tracking the last checkout time


	}
}

