using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sport_Web.Abstraction;
using Sport_Web.Data;
using Sport_Web.DTO;

namespace Sport_Web.Controllers
{
	[ApiController]
	[Route("api/admin/matches")]
	[Authorize(Roles = "Admin")]

	public class MatchesManagementController : ControllerBase
	{
		private readonly IMatchesService _matchesService;

		public MatchesManagementController(IMatchesService matchesService)
		{
			_matchesService = matchesService;	
		}


		[HttpPost("Matches")]
		public async Task<IActionResult> AddMatches([FromBody] MatchesDto matchesDto)
		{
			var matches = await _matchesService.AddMatchesAsync(matchesDto);
			return Ok(matches);
		}

		[HttpPut("UpdateMatches/{id}")]
		public async Task<IActionResult> UpdateMatches(int id, [FromBody] UpdateMatchesDto updateMatchesDto)
		{
			var matches = await _matchesService.UpdateMatchesAsync(id, updateMatchesDto);
			return Ok(matches);
		}

		[HttpDelete("DeleteMatches/{id}")]
		public async Task<IActionResult> DeleteMatches(int id)
		{
			var home = await _matchesService.DeleteMatchesAsync(id);
			return Ok(home);
		}

	
	}


}
