namespace Sport_Web.DTO
{
	public class SubCategoryResponseDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string ImageUrl { get; set; }
		public int? ParentCategoryId { get; set; }
	}
}
