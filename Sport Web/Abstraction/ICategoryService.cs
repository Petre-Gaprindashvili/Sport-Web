using Sport_Web.DTO;
namespace Sport_Web.Abstraction
{
	public interface ICategoryService
	{
		Task<CategoryResponseDto> AddCategoryAsync(CategoryDto categoryDto);
		Task<List<CategoryResponseDto>> GetAllCategories();
		Task<SubCategoryResponseDto> AddSubCategoryAsync(AddSubCategoryDto subCategoryDto);



	}
}

