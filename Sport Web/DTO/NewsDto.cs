using Sport_Web.Models;

namespace Sport_Web.DTO
{
	public class NewsDto
	{
		public string Title { get; set; }
		public string ImageUrl { get; set; }
		public string Content { get; set; }
		public DateTime PublishedDate { get; set; }
		public int CategoryId { get; set; }
		public int? TeamId { get; set; }
		public string? videoUrl { get; set; }	
	}
	public class NewsResponseDto
	{
		public int Id { get; set; }	
		public string Title { get; set; }
		public string ImageUrl { get; set; }
		public string Content { get; set; }
		public DateTime PublishedDate { get; set; }
		public int CategoryId { get; set; }
		public string? videoUrl { get; set; }
		public string parentCategoryName { get; set; }	
	}

}


