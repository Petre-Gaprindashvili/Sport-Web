using Sport_Web.Enums;

namespace Sport_Web.Models
{
	public class CategorySection
	{
		public int Id { get; set; } 
		public string TabName { get; set; }  
		public string CategoryName { get; set; }  
		public int? CategoryId { get; set; }  
		public CategoryType CategoryType { get; set; }	
		public Category Category { get; set; }
		public ICollection<SectionContent> SectionContents { get; set; }

	}
}




