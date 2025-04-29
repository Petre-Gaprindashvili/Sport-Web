using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Sport_Web.Abstraction;
using Sport_Web.Data;
using Sport_Web.DTO;
using Sport_Web.Enums;
using Sport_Web.Models;
using Newtonsoft.Json;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.AspNetCore.Components.Sections;
namespace Sport_Web.Implementation
{
	public class CategoryService : ICategoryService
	{
		private readonly ApplicationDbContext _context;


		public CategoryService( ApplicationDbContext context)
		{
			_context = context;

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

		public async Task<List<CategoryResponseDto>> GetAllCategoriesAsync()
		{
			var getallcategory = _context.Categories.Where(p=>p.ParentCategoryId == null).Select(c => new CategoryResponseDto
			{
				Name = c.Name,
				Id = c.Id
			}).ToList();
			return getallcategory;
		}


		public async Task<CategoryWithSubCategoriesDto> GetCategoryByIdAsync(int id)
		{

			var subCategory = await _context.Categories.Where(c => c.Id == id)
				.Include(category => category.SubCategories).FirstOrDefaultAsync();
			if (subCategory == null) return null;
			var categoryResponse = new CategoryWithSubCategoriesDto
			{
				Id = subCategory.Id,
				Name = subCategory.Name,
				SubCategories = subCategory.SubCategories.Select(c => new SubCategoryResponseDto
				{
					Id = c.Id,
					Name = c.Name,
					ImageUrl = c.ImageUrl,
					ParentCategoryId = c.ParentCategoryId,
				}).ToList()
			};
			return categoryResponse;

		}

		public async Task<CategoryResponseDto> UpdateCategoryAsync(int id, CategoryDto categoryDto)
		{
			var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
			if (category == null) return null;
			category.Name = categoryDto.Name;
			await _context.SaveChangesAsync();

			return new CategoryResponseDto
			{
				Id = category.Id,
				Name = category.Name,
			};
		}

		public async Task<bool> DeleteCategoryAsync(int id)
		{
			var category = _context.Categories.FirstOrDefault(c => c.Id == id);
			if (category == null) return false;
			_context.Categories.Remove(category);
			await _context.SaveChangesAsync();
			return true;

		}



		public async Task<SubCategoryResponseDto> AddSubCategoryAsync(SubCategoryDto subCategoryDto)
		{
			string imageUrl =  subCategoryDto.Image;

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

		public async Task<CategoryWithSubCategoriesDto> GetAllSubCategoryAsync(int id)
		{
			var subCategory = await _context.Categories.Where(c => c.Id == id)
				.Include(category => category.SubCategories).FirstOrDefaultAsync();
			if (subCategory == null) return null;
			var categoryResponse = new CategoryWithSubCategoriesDto
			{
				Id = subCategory.Id,
				Name = subCategory.Name,
				SubCategories = subCategory.SubCategories.Select(c => new SubCategoryResponseDto
				{
					Id = c.Id,
					Name = c.Name, 
					ImageUrl = c.ImageUrl,
					ParentCategoryId = c.ParentCategoryId,
				}).ToList()
			};
			return categoryResponse;

		}

		public async Task<SubCategoryResponseDto> GetSingleSubcategoryByIdAsync(int id)
		{
			var subcategory = await _context.Categories.Where(s => s.Id == id)
				.Select(s => new SubCategoryResponseDto
				{
					Id = s.Id,
					Name = s.Name,
					ImageUrl = s.ImageUrl,
					ParentCategoryId = s.ParentCategoryId,
				}).FirstOrDefaultAsync();

			return subcategory;

		}




		public async Task<SubCategoryResponseDto> UpdateSubCategoryAsync(int id, SubCategoryUpdateDto subCategoryUpdateDto)
		{
			var subCategory = await _context.Categories.Where(c => c.Id == id).FirstOrDefaultAsync();
			if (subCategory == null) return null;
			if (subCategory.ParentCategoryId == null)
			{
				return null;
			}

			subCategory.Name = subCategoryUpdateDto.Name;
			subCategory.ImageUrl = subCategoryUpdateDto.ImageUrl;

			await _context.SaveChangesAsync();

			return new SubCategoryResponseDto
			{
				Id = subCategory.Id,
				Name = subCategory.Name,
				ImageUrl = subCategory.ImageUrl,
				ParentCategoryId = subCategory.ParentCategoryId,
			};

		}

		public async Task<bool> DeleteSubCategoryByIdAsync(int id)
		{
			var subCategory = _context.Categories.FirstOrDefault(c => c.Id == id);
			if (subCategory == null) return false;
			if (subCategory.ParentCategoryId == null) { return false; }
			_context.Categories.Remove(subCategory);
			await _context.SaveChangesAsync();
			return true;
		}



		public async Task<SectionCategortResponseDto> AddSectionAsync(SectionCategoryDto sectionCategoryDto)
		{
			var category = await _context.Categories.FindAsync(sectionCategoryDto.CategoryId);
			if (category == null)
			{
				var response = new ResponseDto { IsSuccess = false, Message = "Category not found" };
			}

			var categoryType = category.ParentCategoryId == null ? CategoryType.ParentCategory : CategoryType.SubCategory;

			var categorySection = new CategorySection
			{
				TabName = sectionCategoryDto.SectionName,
				CategoryId = sectionCategoryDto.CategoryId,
				CategoryName = category.Name,
				CategoryType = categoryType,

			};

			_context.categorySections.Add(categorySection);
			await _context.SaveChangesAsync();


			return new SectionCategortResponseDto
			{

				SectionName = categorySection.TabName,

			};


		}

		public async Task<SectionCategortResponseDto> UpdateSectionAsync(int id, SectionCategoryDto sectionCategoryDto)
		{
			var section = await _context.categorySections.FirstOrDefaultAsync(c => c.Id == id);
			if (section == null) return null;
			section.TabName = sectionCategoryDto.SectionName;	
			_context.categorySections.Update(section);	
			await _context.SaveChangesAsync();
			return new SectionCategortResponseDto
			{
				SectionName = sectionCategoryDto.SectionName,
				CategoryId = sectionCategoryDto.CategoryId,

			};
		}

		public async Task<bool> DeleteSectionAsync(int id)
		{
			var section = await _context.categorySections.FirstOrDefaultAsync(c => c.Id == id);
			if (section == null) return false;
			_context.categorySections.Remove(section);	
			await _context.SaveChangesAsync();
			return true;
		}


		public async Task<List<SectionCategortResponseDto>> GetAllTabsAsync(int categoryId)
		{
			// Get all tabs for a given category
			var tabs = await _context.categorySections
				.Where(tab => tab.CategoryId == categoryId) // Filter by CategoryId
				.Select(tab => new SectionCategortResponseDto
				{
					CategoryId = tab.CategoryId,
					SectionName = tab.TabName, // This will be the tab name, you can map other properties as needed
				})
				.ToListAsync();

			if (tabs == null || !tabs.Any())  // If no tabs found for the given categoryId
			{
				return new List<SectionCategortResponseDto>(); // Return an empty list instead of throwing an exception
			}

			return tabs;  // Return the list of tabs
		}








	}
}






