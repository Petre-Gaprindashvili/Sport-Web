namespace Sport_Web.DTO
{
	public class AddSubCategoryDto
	{
    	public string Name { get; set; }
		public int? ParentCategoryId { get; set; }
		public IFormFile Image { get; set; }
	}
}
