using Microsoft.AspNetCore.Mvc;
using Sport_Web.Abstraction;
using Sport_Web.Data;
using Sport_Web.Implementation;

namespace Sport_Web.Controllers
{
	public class NewsController : Controller
	{
		private readonly INewsService _newsService;	

		public NewsController(INewsService newsService)
		{
			_newsService = newsService;	
		}

		[HttpGet("GetNews/{categoryId}")]
		public async Task<IActionResult> GetNewsByCategory(int categoryId)
		{
			var response = await _newsService.GetNewsByCategoryIdAsync(categoryId);
			return Ok(response);

		}
	}
}

