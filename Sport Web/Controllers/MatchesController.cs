using Microsoft.AspNetCore.Mvc;
using Sport_Web.Abstraction;
using Sport_Web.Data;
using Sport_Web.Implementation;

namespace Sport_Web.Controllers
{
	public class MatchesController : ControllerBase
	{

		private readonly IMatchesService _matchesService;
		private readonly ApplicationDbContext _context;

		public MatchesController(IMatchesService matchesService, ApplicationDbContext dbContext)
		{
			_context = dbContext;
			_matchesService = matchesService;
		}

		[HttpGet("GetMatches/{categoryId}")]
		public async Task<IActionResult> GetMatchesByCategory(int categoryId)
		{
			var response = await _matchesService.GetMatchesByCategoryId(categoryId);
			return Ok(response);

		}

	}
}
