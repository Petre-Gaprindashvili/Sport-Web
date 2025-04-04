using Microsoft.EntityFrameworkCore;
using Sport_Web.Abstraction;
using Sport_Web.Data;
using Sport_Web.DTO;
using Sport_Web.Models;
namespace Sport_Web.Implementation
{
	public class NewsService : INewsService
	{
		private readonly ApplicationDbContext _context;
		private readonly IImageUploadService _imageUploadService;
		public NewsService(ApplicationDbContext context, IImageUploadService imageUploadService)
		{
			_context = context;
			_imageUploadService = imageUploadService;
		}
		public async Task<List<NewsResponseDto>> GetNewsByCategoryIdAsync(int categoryId)
		{

			var news = await _context.Articles
		.Where(t => t.CategoryId == categoryId)
		.Select(t => new NewsResponseDto
		{

			Id = t.Id,
			Title = t.Title,
			Content = t.Content,
			PublishedDate = t.PublishedDate,
			CategoryId = t.CategoryId,
		}).ToListAsync();


			return news;
		}


		public async Task<NewsResponseDto> AddNewsAsync(NewsDto newsDto)
		{
			var sectionContent = await _context.SectionContents.FirstOrDefaultAsync(c => c.Id == newsDto.CategoryId);
			if (sectionContent == null)
			{
				var response = new ResponseDto
				{
					IsSuccess = false,
					Message = "Invalid id."
				};
			}

			string image = await _imageUploadService.UploadImageAsync(newsDto.ImageUrl);

			var news = new Articles
			{


				Title = newsDto.Title,
				Content = newsDto.Content,
				ImageUrl = image,
				PublishedDate = newsDto.PublishedDate,
				CategoryId = newsDto.CategoryId,
			};
			_context.Articles.Add(news);
			await _context.SaveChangesAsync();

			return new NewsResponseDto
			{
				Id = news.Id,
				Title = news.Title,
				Content = news.Content,
				PublishedDate = news.PublishedDate,
				CategoryId = news.CategoryId,
				ImageUrl = news.ImageUrl,

			};

		}



		public async Task<NewsResponseDto> UpdateNewsAsync(int id, NewsDto newsDto)
		{
			var news = await _context.Articles.FirstOrDefaultAsync(c => c.Id == id);
			if (news == null)
			{
				var response = new ResponseDto
				{
					IsSuccess = false,
					Message = "Invalid Id"
				};
			}

			string updatedPhotoUrl = news.ImageUrl;

			updatedPhotoUrl = await _imageUploadService.UploadImageAsync(newsDto.ImageUrl);


			news.Title = newsDto.Title;
			news.Content = newsDto.Content;
			news.PublishedDate = newsDto.PublishedDate;
			news.ImageUrl = updatedPhotoUrl;
			await _context.SaveChangesAsync();
			return new NewsResponseDto
			{
				Id = news.Id,
				Title = news.Title,
				Content = news.Content,
				ImageUrl = news.ImageUrl,

			};
		}

		public async Task<bool> DeleteNewsAsync(int id)
		{
			var news = _context.Articles.FirstOrDefault(h => h.Id == id);
			if (news == null)
			{
				var response = new ResponseDto
				{
					IsSuccess = false,
					Message = "Invalid Id"
				};
			}
			_context.Articles.Remove(news);
			await _context.SaveChangesAsync();
			return true;
		}

	}
}
