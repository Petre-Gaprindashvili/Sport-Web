using Microsoft.AspNetCore.Mvc;
using Sport_Web.Abstraction;
using Sport_Web.Data;
using Sport_Web.Implementation;

namespace Sport_Web.Controllers
{
	[ApiController]
	[Route("api/teams")]
	public class TeamController : ControllerBase
	{

		private readonly IteamsService _iteamsService;
		private readonly ApplicationDbContext _context;

		public TeamController(IteamsService iteamsService)
		{
		
			_iteamsService = iteamsService;
		}

		[HttpGet("GetAllTeams/{categoryId}")]
		public async Task<IActionResult> GetTeamByCategory(int categoryId)
		{
			var response = await _iteamsService.GetAllTeamByCategoryIdAsync(categoryId);
			return Ok(response);

		}

		[HttpGet("GetSingleTeam/{id}")]
		public async Task<IActionResult> GetSingleTeam(int id)
		{
			var response = await _iteamsService.GetSingleTeamByIdAsync(id);
			return Ok(response);

		}



	}
}
