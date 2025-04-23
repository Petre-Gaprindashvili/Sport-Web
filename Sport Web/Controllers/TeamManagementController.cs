using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sport_Web.Abstraction;
using Sport_Web.Data;
using Sport_Web.DTO;
using Sport_Web.Implementation;

namespace Sport_Web.Controllers
{
	[ApiController]
	[Route("api/admin/teams")]
	[Authorize(Roles = "Admin")]

	public class TeamManagementController : ControllerBase
	{

		private readonly IteamsService _iteamsService;

		public TeamManagementController(IteamsService iteamsService)
		{
			
			_iteamsService = iteamsService;	
		}


		[HttpPost("Team")]
		public async Task<IActionResult> AddTeam(TeamDto teamDto)
		{
			var team = await _iteamsService.AddTeamAsync(teamDto);
			return Ok(team);
		}

		[HttpPut("UpdateTeam/{id}")]
		public async Task<IActionResult> UpdateTeam(int id, TeamDto teamDto)
		{
			var teams = await _iteamsService.UpdateTeamAsync(id, teamDto);
			return Ok(teams);
		}

		[HttpDelete("DeleteTeam/{id}")]
		public async Task<IActionResult> DeleteTeam(int id)
		{
			var team = await _iteamsService.DeleteTeamAsync(id);
			return Ok(team);
		}
	}
}
