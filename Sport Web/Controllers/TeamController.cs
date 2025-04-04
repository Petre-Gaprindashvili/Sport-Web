using Microsoft.AspNetCore.Mvc;
using Sport_Web.Abstraction;
using Sport_Web.Data;
using Sport_Web.Implementation;

namespace Sport_Web.Controllers
{
	public class TeamController : ControllerBase
	{

		private readonly IteamsService _iteamsService;
		private readonly ApplicationDbContext _context;

		public TeamController(IteamsService iteamsService, ApplicationDbContext dbContext)
		{
			_context = dbContext;	
		}

		[HttpGet("GetTeam/{categoryId}")]
		public async Task<IActionResult> GetTeamByCategory(int categoryId)
		{
			var response = await _iteamsService.GetTeamByCategoryIdAsync(categoryId);
			return Ok(response);

		}




	}
}
