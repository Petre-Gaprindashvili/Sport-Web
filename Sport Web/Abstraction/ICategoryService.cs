using Sport_Web.DTO;
using Sport_Web.Models;
namespace Sport_Web.Abstraction
{
	public interface ICategoryService
	{
		Task<List<CategoryResponseDto>> GetAllCategories();
		Task<CategoryWithSubCategoriesDto> GetCategoryById(int id);
		// Admin-specific methods:
		Task<CategoryResponseDto> AddCategoryAsync(CategoryDto categoryDto);
		Task<CategoryResponseDto> UpdateCategoryAsync(int id, CategoryDto categoryDto);	
		Task<bool> DeleteCategoryAsync(int id);	
		Task<SubCategoryResponseDto> AddSubCategoryAsync(AddSubCategoryDto subCategoryDto);
		Task<SubCategoryResponseDto> UpdateSubCategoryAsync(int id, SubCategoryUpdateDto subCategoryUpdate);
		Task<bool> DeleteSubCategoryByIdAsync(int id);	
		Task<TabsForCategoryResponseDto> AddTabForCategoryAsync(TabsForCategoryDto tabsForCategoryDto);
		Task<List<TabsForCategoryResponseDto>> GetParentCategoryTabsAsync(int parentCatgoryId);


	}
}

