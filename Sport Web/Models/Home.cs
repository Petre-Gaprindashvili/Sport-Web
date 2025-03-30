namespace Sport_Web.Models
{
	public class Home
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string ImageUrl { get; set; }
		public int SectionContentId { get; set; }
		public Category Category { get; set; }
		public SectionContent SectionContent { get; set; }

	}
}



