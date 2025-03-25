using Sport_Web.Enums;

namespace Sport_Web.DTO
{
	public class TabsForCategoryResponseDto
	{
		//public int Id { get; set; }	
		public string TabName { get; set; }
		public string CategoryName { get; set; }	
		public int CategoryId { get; set; }
		public string CategoryType { get; set; }	
	}
}
