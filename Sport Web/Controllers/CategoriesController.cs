using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Writers;
using Sport_Web.Abstraction;

namespace Sport_Web.Controllers
{

	[ApiController]
	[Route("api/categories")]
	public class CategoriesController : ControllerBase
	{
		private readonly ICategoryService _categoryService;
		public CategoriesController(ICategoryService categoryService)
		{
			_categoryService = categoryService;

		}

		[HttpGet("GetAllCategory")]
		public async Task<IActionResult> GetAllCategories()
		{
			var getAllCategory = await _categoryService.GetAllCategoriesAsync();

			if (getAllCategory == null)
			{
				return BadRequest("not found");
			}
			return Ok(getAllCategory);

		}

		[HttpGet("GetAllSubCategory/{id}")]
		public async Task<IActionResult> GetAllSubCategory(int id)
		{
			var getAllSubCategory = await _categoryService.GetAllSubCategoryAsync(id);
			if (getAllSubCategory == null) return BadRequest("Invalid id");
			return Ok(getAllSubCategory);
		}

		[HttpGet("GetSingleSubcategory/{id}")]
		public async Task<IActionResult> GetSingleSubcategory(int id)
		{
			var getSingleSubCategory = await _categoryService.GetSingleSubcategoryByIdAsync(id);
			if (getSingleSubCategory == null) return BadRequest("Invalid id");
			return Ok(getSingleSubCategory);
		}


		[HttpGet("GetAllTabs")]
		public async Task<IActionResult> GetTabsByCategoryId(int categoryId)
		{
			try
			{
				var tabs = await _categoryService.GetAllTabsAsync(categoryId);

				if (!tabs.Any())  // If no tabs are returned
				{
					return NotFound(new { message = "No tabs found for this category." });
				}

				return Ok(tabs);  // Return the found tabs
			}
			catch (Exception ex)
			{
				// Log the exception and return a general server error
				return StatusCode(500, new { message = ex.Message });
			}
		}

	}
}

