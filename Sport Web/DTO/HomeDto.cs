namespace Sport_Web.DTO
{
	public class HomeDto
	{
		public string Tittlee { get; set; }
		public IFormFile LogoUrl { get; set; }
		public string Description { get; set; }
		public int CategorySectionId { get; set; }

	}
	public class HomeResponseDto : ContentBase
	{
		public string SectionNamea { get; set; }
		public string Tittle { get; set; }
		public string LogoUrl { get; set; }



	}
}

public class ContentBase
{
	public int Id { get; set; }
	public string SectionName { get; set; }
	public int CategorySectionId { get; set; }

}

