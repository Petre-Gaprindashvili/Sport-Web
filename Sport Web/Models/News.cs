using System.ComponentModel.DataAnnotations;

namespace Sport_Web.Models
{
	public class News
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public int CategoryId { get; set; }
		public string Image { get; set; }
		public string Content { get; set; }
		public DateTime PublishedDate { get; set; }
		public Category Category { get; set; }
		public int? TeamId { get; set; }
		public string? VideoUrl { get; set; } 

		public Team Teams { get; set; }		

		//public CategorySection CategorySection { get; set; }

		//public SectionContent SectionContent { get; set; }
	}
}




