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

		[HttpGet("GetCategory{id}")]
		public async Task<IActionResult> GetCategory(int id)
		{
			var getCategory = await _categoryService.GetCategoryByIdAsync(id);
			if (getCategory == null) return BadRequest("Invalid id");
			return Ok(getCategory);
		}


		[HttpGet("GetAllTabs")]
		public async Task<IActionResult> GetTabsByCategoryId()
		{
			var tabs = await _categoryService.GetAllTabsAsync();
			return Ok(tabs);
		}
	}
}

