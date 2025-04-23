using Microsoft.AspNetCore.Components.Sections;
using Sport_Web.Abstraction;

namespace Sport_Web.DTO
{
	public class TeamDto 
	{ 
		public string TeamName { get; set; }
		public string LogoUrl { get; set; }	
		public int CategoryId { get; set; }
	}
	public class TeamResponseDto 
	{
		public int Id { get; set; }	
		public string TeamName { get; set; }
		public string LogoUrl { get; set; }
		public int CategoryId { get; set; }



	}
}






