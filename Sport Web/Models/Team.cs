using System.Numerics;

namespace Sport_Web.Models
{
	public class Team
	{
		public int Id { get; set; }
		public  string Name { get; set; }
		public int CategoryId { get; set; }
		public string? LogoUrl { get; set; }
		public Category Category { get; set; }

		public ICollection<Player>? Players { get; set; }
		public ICollection<News> News { get; set; }	
		public ICollection<Product> Products { get; set; }

		public ICollection<Match> HomeMatches { get; set; }
	    public ICollection<Match> AwayMatches { get; set; }

	}
}





























