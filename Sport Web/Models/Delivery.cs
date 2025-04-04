using Sport_Web.Enums;
namespace Sport_Web.Models
{
	public class Delivery
	{
		public int Id { get; set; }
		public int CartId { get; set; }
		public Cart Cart { get; set; }
		public DeliveryStatus Status { get; set; } = DeliveryStatus.Received;	
		public DateTime DeliveryDate { get; set; }
	}
}
