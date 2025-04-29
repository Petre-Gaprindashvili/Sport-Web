using Sport_Web.DTO;
using Sport_Web.Abstraction;
using Sport_Web.Models;
using System.Collections;
using System.Text.Json.Serialization;

namespace Sport_Web.DTO
{
	public class SectionCategoryDto
	{
      	public string SectionName { get; set; }
		public int CategoryId { get; set; }

		}
	

	public class SectionCategortResponseDto
	{
		public string SectionName { get; set; }
		public int? CategoryId { get; set; }
	}



	public class TabContentDto
	{
		public int SectionId { get; set; }
		public string SectionName { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public HomeResponseDto HomeContent { get; set; }
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public TeamResponseDto TaemContent { get; set; }
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

		public MatchesResponseDto MatchesContent { get; set; }


	}
}

















