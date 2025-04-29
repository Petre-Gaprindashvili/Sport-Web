using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sport_Web.Abstraction;
using Sport_Web.Data;
using Sport_Web.Implementation;

namespace Sport_Web.Controllers
{
	[ApiController]
	[Route("api/news")]
	

	public class NewsController : Controller
	{
		private readonly INewsService _newsService;	

		public NewsController(INewsService newsService)
		{
			_newsService = newsService;	
		}

		[HttpGet("GetNewsByParentCategory")]
		public async Task<IActionResult> GrtNewsByParentCategoryId()
		{
			var response = await _newsService.GetNewsByParentCategoryAsync();
			return Ok(response);

		}

		[HttpGet("GetNewsByCategory/{categoryId}")]
		public async Task<IActionResult> GetNewsByCategory(int categoryId)
		{
			var response = await _newsService.GetNewsByCategoryIdAsync(categoryId);
			return Ok(response);

		}

		[HttpGet("GetNewsByTeam/{teamId}")]
		public async Task<IActionResult> GetNewsByTeam(int teamId)
		{
			var response = await _newsService.GetNewsByTeamIdAsync(teamId);
			return Ok(response);

		}

		[HttpGet("GetVideoNewsByCategory/{categoryId}")]
		public async Task<IActionResult> GetVideoNews(int categoryId)
		{
			var response = await _newsService.GetVideoNewsByCategoryIdAsync(categoryId);
			return Ok(response);

		}
	}
}

