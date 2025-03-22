using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sport_Web.Abstraction;
using Sport_Web.DTO;
using Swashbuckle.AspNetCore.Annotations;

namespace Sport_Web.Controllers
{
	[ApiController]
	[Route("api/admin/categories")]
	[Authorize(Roles = "Admin")]
	public class AdminCategoriesController : ControllerBase
	{
		private readonly ICategoryService _categoryService;
		public AdminCategoriesController(ICategoryService categoryService)
		{
			_categoryService = categoryService;

		}
		[HttpPost("add-category")]
		public async Task<IActionResult> AddCategory([FromBody] CategoryDto categoryDto)
		{

			var category = await _categoryService.AddCategoryAsync(categoryDto);
			if (category == null)
			{
				return BadRequest("kkkkk");
			}
			return Ok(categoryDto);
		}

		[HttpGet("get-all-gategories")]
		public async Task<IActionResult> GetAllCategories()
		{
			var getAllCategory = await _categoryService.GetAllCategories();

			if (getAllCategory == null)
			{
				return BadRequest("not found");
			}
			return Ok(getAllCategory);


		}



		[HttpPost("add-subcategory")]
		public async Task<IActionResult> AddSubCategory([FromForm] AddSubCategoryDto subCategoryDto)
		{
			var subCategory = await _categoryService.AddSubCategoryAsync(subCategoryDto);
			if (subCategory == null) return BadRequest();
			return Ok(subCategory);
		}

		

	}

}

