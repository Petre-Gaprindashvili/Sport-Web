//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Sport_Web.Abstraction;
//using Sport_Web.Data;
//using Sport_Web.DTO;
//using Sport_Web.Implementation;
//using Sport_Web.Models;

//namespace Sport_Web.Controllers
//{
//	[ApiController]
//	[Route("api/content")]
//	public class ContentController : ControllerBase
//	{
//		private readonly IcontentService _contentService;
//		private readonly ApplicationDbContext _context;

//		public ContentController(IcontentService contentService, ApplicationDbContext dbContext)
//		{
//			_contentService = contentService;
//		}

//		//[HttpGet("GetHome/{categoryId}")]
//		//public async Task<IActionResult> GetHomeByCategory(int categoryId)
//		//{
//		//	var response = await _contentService.GetHomeByCategoryIdAsync(categoryId);
//		//	return Ok(response);

//		//}

//		[HttpGet("GetTeam/{categoryId}")]
//		public async Task<IActionResult> GetTeamByCategory(int categoryId)
//		{
//			var response = await _contentService.GetTeamByCategoryIdAsync(categoryId);
//			return Ok(response);

//		}

//		[HttpGet("GetMatches/{categoryId}")]
//		public async Task<IActionResult> GetMatchesByCategory(int categoryId)
//		{
//			var response = await _contentService.GetMatchesByCategoryId(categoryId);
//			return Ok(response);

//		}


//		[HttpGet("GetNews/{categoryId}")]
//		public async Task<IActionResult> GetNewsByCategory(int categoryId)
//		{
//			var response = await _contentService.GetNewsByCategoryIdAsync(categoryId);
//			return Ok(response);

//		}



//		[HttpGet("GetPlayer/{teamId}")]
//		public async Task<ActionResult<List<PlayersResponseDto>>> GetPlayersByTeam(int teamId)
//		{
//			var players = await _contentService.GetPlayersByTeamIdAsync(teamId);
//			return Ok(players);
//		}



//		[HttpGet("GetProduct/{teamId}")]
//		public async Task<ActionResult<List<PlayersResponseDto>>> GetProductByTeam(int teamId)
//		{
//			var products = await _contentService.GetProductsByTeamIdAsync(teamId);
//			return Ok(products);
//		}
//	}

//}









