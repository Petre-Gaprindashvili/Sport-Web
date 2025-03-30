using Microsoft.AspNetCore.Components.Sections;
using Sport_Web.Abstraction;

namespace Sport_Web.DTO
{
	public class TeamDto 
	{ 
		public string TeamName { get; set; }
		public int CategorySectionId { get; set; }
		public IFormFile LogoUrl { get; set; }	
	}
	public class TeamResponseDto : ContentBase
	{
		//public int Id { get; set; }
		public string TeamName { get; set; }
		public string tabname { get; set; }		
		public string LogoUrl { get; set; }
		//public int? CategorySectionId { get; set; }


		
	}
}


//public class ContentBase
//{
//	public int Id { get; set; }                 
//	public string SectionName { get; set; }          
//	public int CategorySectionId { get; set; }

//}



