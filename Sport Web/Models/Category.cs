using System.Text.Json.Serialization;

namespace Sport_Web.Models
{
	public class Category
	{
		public int Id { get; set; }
		public required string Name { get; set; }
		public int? ParentCategoryId { get; set; }

		[JsonIgnore]
		public Category ParentCategory { get; set; }

		[JsonIgnore]
		public ICollection<Category> SubCategories { get; set; } 

		public ICollection<Team> Teams { get; set; } 

		public string? ImageUrl { get; set; }

	}
}
