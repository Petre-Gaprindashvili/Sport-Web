using Microsoft.AspNetCore.Mvc;
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

			var news = await _context.News
		.Where(t => t.CategoryId == categoryId)
		.Select(t => new NewsResponseDto
		{

			Id = t.Id,
			Title = t.Title,
			Content = t.Content,
			ImageUrl = t.Image,	
			PublishedDate = t.PublishedDate,
			CategoryId = t.CategoryId,
			videoUrl = t.VideoUrl,
		}).ToListAsync();


			return news;
		}

		public async Task<List<NewsResponseDto>> GetNewsByTeamIdAsync(int teamId)
		{
			var news = await _context.News.Where(t => t.TeamId == teamId).
				Select(t => new NewsResponseDto
				{
					Id = t.Id,
					Title = t.Title,
					ImageUrl = t.Image,
					Content = t.Content,
					PublishedDate = t.PublishedDate,
					CategoryId = t.CategoryId,
					videoUrl = t.VideoUrl,	
				}).ToListAsync();
			return news;
		}

		public async Task<List< NewsResponseDto>> GetVideoNewsByCategoryIdAsync(int categoryId)
		{
			var videoNews = await _context.News
				.Where(n => n.CategoryId == categoryId && !string.IsNullOrEmpty(n.VideoUrl))
				.OrderByDescending(n => n.PublishedDate)  // Optional: sort by published date
				.ToListAsync();
			var videoNewsDto = videoNews.Select(n=> new NewsResponseDto
			{
				Id = n.Id,
				Title = n.Title,
				videoUrl = n.VideoUrl,
				Content = n.Content,
				PublishedDate = n.PublishedDate,
				CategoryId = n.CategoryId,
			}).ToList();	

			return videoNewsDto;  // Return only news items with videos
		}

		public async Task<NewsResponseDto> AddNewsAsync(NewsDto newsDto)
		{
			var sectionContent = await _context.categorySections
				.FirstOrDefaultAsync(c => c.CategoryId == newsDto.CategoryId);

			if (sectionContent == null) return null;

			string image = newsDto.ImageUrl;

			var news = new News
			{
				Title = newsDto.Title,
				Content = newsDto.Content,
				Image = image,
				PublishedDate = newsDto.PublishedDate,
				CategoryId = newsDto.CategoryId,
				TeamId = newsDto.TeamId,
				VideoUrl = newsDto.videoUrl,
			};

			_context.News.Add(news);
			await _context.SaveChangesAsync();

			return new NewsResponseDto
			{
				Id = news.Id,
				Title = news.Title,
				Content = news.Content,
				PublishedDate = news.PublishedDate,
				CategoryId = news.CategoryId,
				ImageUrl = news.Image,
				videoUrl = news.VideoUrl,	
			    
				// Optionally add TeamId to the response DTO if needed
			};
		}



		//public async Task<NewsResponseDto> AddNewsAsync(NewsDto newsDto)
		//{
		//	var sectionContent = await _context.categorySections.FirstOrDefaultAsync(c => c.CategoryId == newsDto.CategoryId);
		//	if (sectionContent == null) return null;


		//	string image =  newsDto.ImageUrl;

		//	var news = new News
		//	{


		//		Title = newsDto.Title,
		//		Content = newsDto.Content,
		//		Image = image,
		//		PublishedDate = newsDto.PublishedDate,
		//		CategoryId = newsDto.CategoryId,
		//	};
		//	_context.News.Add(news);
		//	await _context.SaveChangesAsync();

		//	return new NewsResponseDto
		//	{
		//		Id = news.Id,
		//		Title = news.Title,
		//		Content = news.Content,
		//		PublishedDate = news.PublishedDate,
		//		CategoryId = news.CategoryId,
		//		ImageUrl = news.Image,

		//	};

		//}



		public async Task<NewsResponseDto> UpdateNewsAsync(int id, NewsDto newsDto)
		{
			var news = await _context.News.FirstOrDefaultAsync(c => c.Id == id);
			if (news == null)
			{
				var response = new ResponseDto
				{
					IsSuccess = false,
					Message = "Invalid Id"
				};
			}

			string updatedPhotoUrl = news.Image;

			updatedPhotoUrl = newsDto.ImageUrl;


			news.Title = newsDto.Title;
			news.Content = newsDto.Content;
			news.PublishedDate = newsDto.PublishedDate;
			news.Image = updatedPhotoUrl;
			await _context.SaveChangesAsync();
			return new NewsResponseDto
			{
				Id = news.Id,
				Title = news.Title,
				Content = news.Content,
				ImageUrl = news.Image,

			};
		}

		public async Task<bool> DeleteNewsAsync(int id)
		{
			var news = _context.News.FirstOrDefault(h => h.Id == id);
			if (news == null)
			{
				var response = new ResponseDto
				{
					IsSuccess = false,
					Message = "Invalid Id"
				};
			}
			_context.News.Remove(news);
			await _context.SaveChangesAsync();
			return true;
		}



	}
}
