namespace Sport_Web.DTO
{

	public class CategoryWithSubCategoriesDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public List<SubCategoryResponseDto> SubCategories { get; set; }
	}
	public class SubCategoryResponseDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string ImageUrl { get; set; }
		public int? ParentCategoryId { get; set; }
	}
}

public class SubCategoryUpdateDto
{
	public string Name { get; set; }
	public string ImageUrl { get; set; }

}

public class TabsForCategoryDto
{
	public string TabName { get; set; }
	public int CategoryId { get; set; }
}