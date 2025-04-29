using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Sport_Web.Abstraction;
using Sport_Web.Data;
using Sport_Web.DTO;
using Sport_Web.Implementation;
using Sport_Web.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json;

namespace Sport_Web.Controllers
{
	[ApiController]
	[Route("api/admin/categories")]
	[Authorize(Roles = "Admin")]
	public class CategoryManagementController : ControllerBase
	{
		private readonly ICategoryService _categoryService;

		public CategoryManagementController( ICategoryService categoryService)
		{
			_categoryService = categoryService;
		
		}
		[HttpPost("AddNewCategory")]
		public async Task<IActionResult> AddCategory( CategoryDto categoryDto)
		{

			var category = await _categoryService.AddCategoryAsync(categoryDto);
			if (category == null)
			{
				return BadRequest();
			}
			return Ok(categoryDto);
		}



		[HttpPut("UpdateCategory{id}")]
		public async Task<IActionResult> UpdateCategory(int id, CategoryDto categoryDto)
		{
			var UpdateCategory = await _categoryService.UpdateCategoryAsync(id, categoryDto);
			if (UpdateCategory == null) return NotFound("Invalid Id");
			return Ok(UpdateCategory);
		}

		[HttpDelete("DeleteCategory{id}")]
		public async Task<IActionResult> DeleteCategory(int id)
		{
			var category = await _categoryService.DeleteCategoryAsync(id);
			if (category == null) return NotFound($"Category with ID {id} not found.");
			return Ok(category);

		}


		[HttpPost("AddSubCategory")]
		public async Task<IActionResult> AddSubCategory(SubCategoryDto subCategoryDto)
		{
			var subCategory = await _categoryService.AddSubCategoryAsync(subCategoryDto);
			if (subCategory == null) return BadRequest();
			return Ok(subCategory);
		}


		[HttpPut("UpdateSubCategory{id}")]
		public async Task<IActionResult> UpdateSubCategory(int id, SubCategoryUpdateDto subCategoryUpdateDto)
		{
			var updateSubCategory = await _categoryService.UpdateSubCategoryAsync(id, subCategoryUpdateDto);
			if (updateSubCategory == null) return NotFound("Invalid Id");
			return Ok(updateSubCategory);
		}

		[HttpDelete("DeleteSubCategory{id}")]
		public async Task<IActionResult> DeleteSubCategory(int id)
		{
			var subCategory = await _categoryService.DeleteSubCategoryByIdAsync(id);
			if (subCategory == null) return NotFound("Incorrect Id");
			return Ok(subCategory);

		}

		[HttpPost("AddSection")]
		public async Task<IActionResult> AddTabForCategory(SectionCategoryDto sectionCategoryDto)
		{
			var subCategoryItem = await _categoryService.AddSectionAsync(sectionCategoryDto);
			if (subCategoryItem == null)
			{
				return BadRequest("Invalid data");
			}
			return Ok(subCategoryItem);
		}


		[HttpPut("UpdateSection{id}")]
		public async Task<IActionResult> UpdateSection(int id, SectionCategoryDto sectionCategoryDto)
		{
			var updateSection = await _categoryService.UpdateSectionAsync(id, sectionCategoryDto);
			return Ok(updateSection);
		}


		[HttpDelete("DeleteSecton{id}")]
		public async Task<IActionResult> DeleteSection(int id)
		{
			var section = await _categoryService.DeleteSectionAsync(id);
			return Ok(section);

		}


	}

}




