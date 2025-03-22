using System.Numerics;

namespace Sport_Web.Models
{
	public class Team
	{

		public int Id { get; set; }
		public required string Name { get; set; }
		public int CategoryId { get; set; } 

		public Category Category { get; set; }
		public ICollection<Player>? Players { get; set; } 


		public string? LogoUrl { get; set; }
	}
}
