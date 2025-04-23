using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace Sport_Web.DTO
{
	public class SubCategoryDto
	{
    	public string Name { get; set; }
		public int ParentCategoryId { get; set; }
		public string Image { get; set; }
	}

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

	public class SubCategoryUpdateDto
	{
		public string Name { get; set; }
		public string ImageUrl { get; set; }

	}

}
