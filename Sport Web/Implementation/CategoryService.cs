using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Sport_Web.Abstraction;
using Sport_Web.Data;
using Sport_Web.DTO;
using Sport_Web.Models;
namespace Sport_Web.Implementation
{
	public class CategoryService : ICategoryService
	{
		private readonly ApplicationDbContext _context;
		private readonly IImageUploadService _imageUploadService;


		public CategoryService(IImageUploadService imageUploadService, ApplicationDbContext context)
		{
			_context = context;
			_imageUploadService = imageUploadService;

		}

		public async Task<CategoryResponseDto> AddCategoryAsync(CategoryDto categoryDto)
		{
			if (string.IsNullOrWhiteSpace(categoryDto.Name))
				throw new ArgumentException("Category name is required.");

			var category = new Category
			{

				Name = categoryDto.Name,

			};

			_context.Categories.Add(category);
			await _context.SaveChangesAsync();

			var categoryResponse = new CategoryResponseDto
			{
				Id = category.Id,
				Name = category.Name,
			};
			return categoryResponse;


		}

		public async Task<List<CategoryResponseDto>> GetAllCategories()
		{
			var getallcategory = _context.Categories.Select(c => new CategoryResponseDto
			{
				Name = c.Name,
				Id = c.Id
			}).ToList();
			return getallcategory;
		}

		public async Task<SubCategoryResponseDto> AddSubCategoryAsync(AddSubCategoryDto subCategoryDto)
		{
			string imageUrl = await _imageUploadService.UploadImageAsync(subCategoryDto.Image);

			var subCategory = new Category
			{
				Name = subCategoryDto.Name,
				ParentCategoryId = subCategoryDto.ParentCategoryId,
				ImageUrl = imageUrl,
			};
			_context.Categories.Add(subCategory);
			await _context.SaveChangesAsync();

			return new SubCategoryResponseDto
			{
				Id = subCategory.Id,
				Name = subCategory.Name,
				ParentCategoryId = subCategory.ParentCategoryId,
				ImageUrl = imageUrl,

			};

		}


	}
}


