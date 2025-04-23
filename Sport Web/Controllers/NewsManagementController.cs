using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sport_Web.Abstraction;
using Sport_Web.DTO;
using Sport_Web.Implementation;

namespace Sport_Web.Controllers
{
	[ApiController]
	[Route("api/admin/news")]
	[Authorize(Roles = "Admin")]

	public class NewsManagementController : Controller
	{

		private readonly INewsService _newsService;

		public NewsManagementController(INewsService newsService)
		{
			_newsService = newsService;
		}


		[HttpPost("News")]
		public async Task<IActionResult> AddNews(NewsDto newsDto)
		{
			var news = await _newsService.AddNewsAsync(newsDto);
			return Ok(news);
		}

		[HttpPut("UpdateNews/{id}")]
		public async Task<IActionResult> UpdateNews(int id, NewsDto newsDto)
		{
			var updatedNews = await _newsService.UpdateNewsAsync(id, newsDto);
			return Ok(updatedNews);
		}

		[HttpDelete("DeleteNews/{id}")]
		public async Task<IActionResult> DeleteNews(int id)
		{
			var news = await _newsService.DeleteNewsAsync(id);
			return Ok(news);
		}
	}
}

