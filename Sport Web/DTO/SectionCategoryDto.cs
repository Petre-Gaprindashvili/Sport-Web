using Sport_Web.DTO;
using Sport_Web.Abstraction;
using Sport_Web.Models;
using System.Collections;

namespace Sport_Web.DTO
{
	public class SectionCategoryDto
	{
      	public string SectionName { get; set; }
		public int CategoryId { get; set; }

		}
	

	public class SectionCategortResponseDto
	{
		//public int Id { get; set; }	
		public string SectionName { get; set; }
		public int? CategoryId { get; set; }
	}

	public class TabContentResponseDto
	{
		public int TabId { get; set; }
		public string TabName { get; set; }
		//public TeamResponseDto Team { get; set; }

	}

	
	public class TabContentDto
	{
		public int SectionId { get; set; }
		public ContentBase Content { get; set; } 


		//public  List< Team> content { get; set; }


	}
}













