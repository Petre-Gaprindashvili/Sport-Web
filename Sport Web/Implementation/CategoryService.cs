using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Sport_Web.Abstraction;
using Sport_Web.Data;
using Sport_Web.DTO;
using Sport_Web.Enums;
using Sport_Web.Models;
using System.Linq.Expressions;
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


		public async Task<CategoryWithSubCategoriesDto> GetCategoryById(int id)
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
			//var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
			//if (category == null) return null;

			//return new CategoryResponseDto
			//{
			//	Id = category.Id,
			//	Name = category.Name,
			//};

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

		public async Task<CategoryWithSubCategoriesDto> GetSubCategoryByIdAsync(int id)
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


		public async Task<TabsForCategoryResponseDto> AddTabForCategoryAsync(TabsForCategoryDto tabsForCategoryDto)
		{
			var category = await _context.Categories.FindAsync(tabsForCategoryDto.CategoryId);
			if (category == null)
			{
				var response = new AuthResponseDto { IsSuccess =  false,  Message = "Category not found" };
			}

			var categoryType = category.ParentCategoryId == null ? CategoryType.ParentCategory : CategoryType.SubCategory;

			var categoryTab = new CategoryTab
			{
				TabName = tabsForCategoryDto.TabName,
				CategoryId = tabsForCategoryDto.CategoryId,
				CategoryName = category.Name,
				CategoryType = categoryType,
				
				

			};

			_context.CategoryTabs.Add(categoryTab);
			await _context.SaveChangesAsync();


			 return  new TabsForCategoryResponseDto
			{

      			TabName = categoryTab.TabName,
				CategoryId = categoryTab.CategoryId,
				CategoryName = categoryTab.CategoryName,
				CategoryType = categoryTab.CategoryType.ToString(),
			};
			
			}

		public async Task<List<TabsForCategoryResponseDto>> GetParentCategoryTabsAsync(int parentCategoryId)
		{
			var tabs = await _context.CategoryTabs.Where(tab=> tab.CategoryId == parentCategoryId
			&& tab.CategoryType == CategoryType.ParentCategory).ToListAsync();
			if(tabs == null)
			{
				var response = new AuthResponseDto
				{
					IsSuccess = false,
					Message = "Tabs for parentcategory not found"
				};
			}

			return tabs.Select(tab=> new TabsForCategoryResponseDto
			{
		       TabName=tab.TabName,		
			   CategoryId=tab.CategoryId,	
			   CategoryName=tab.CategoryName,	
			   CategoryType = tab.CategoryType.ToString(),	
			}).ToList();

		}


	}
}




