using Sport_Web.DTO;
using Sport_Web.Enums;
using Sport_Web.Models;
namespace Sport_Web.Abstraction
{
	public interface ICategoryService
	{
		Task<List<CategoryResponseDto>> GetAllCategoriesAsync();
		Task<CategoryWithSubCategoriesDto> GetAllSubCategoryAsync(int id);
		Task<SubCategoryResponseDto> GetSingleSubcategoryByIdAsync(int id);	
		Task< List<SectionCategortResponseDto>> GetAllTabsAsync(int categoryId);


		// Admin-specific methods:
		Task<CategoryResponseDto> AddCategoryAsync(CategoryDto categoryDto);
		Task<CategoryResponseDto> UpdateCategoryAsync(int id, CategoryDto categoryDto);	
		Task<bool> DeleteCategoryAsync(int id);	
		Task<SubCategoryResponseDto> AddSubCategoryAsync(SubCategoryDto subCategoryDto);
		Task<SubCategoryResponseDto> UpdateSubCategoryAsync(int id, SubCategoryUpdateDto subCategoryUpdate);
		Task<bool> DeleteSubCategoryByIdAsync(int id);	
		Task<SectionCategortResponseDto> AddSectionAsync(SectionCategoryDto sectionCategoryDto);
		Task<SectionCategortResponseDto> UpdateSectionAsync(int id, SectionCategoryDto sectionCategoryDto);
		Task<bool> DeleteSectionAsync(int id);

	}
}

