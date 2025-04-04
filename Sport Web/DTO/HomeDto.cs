using Sport_Web.DTO;

namespace Sport_Web.DTO
{
	public class HomeDto
	{
		public string Tittle { get; set; }
		public IFormFile LogoUrl { get; set; }
		public string Description { get; set; }
		public int CategorySectionId { get; set; }

	}
	public class HomeResponseDto
	{
		public int Id { get; set; }
		public string Tittle { get; set; }
		public string Description { get; set; }
		public string LogoUrl { get; set; }
		public int CategoryId { get; set; }



		}
	}





