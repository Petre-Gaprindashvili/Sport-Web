using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace Sport_Web.Models
{
	public class SectionContent
	{

		public int Id { get; set; }
		public string? ContentType { get; set; }
		public int CategorySectionId { get; set; }
		public CategorySection CategorySection { get; set; }
		public ICollection<Home> HomeContent { get; set; }		
		public  ICollection <Team> Teams { get; set; }
	}
}



