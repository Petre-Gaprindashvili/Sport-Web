using Microsoft.AspNetCore.Mvc;
using Sport_Web.Abstraction;
using Sport_Web.Data;
using Sport_Web.DTO;

namespace Sport_Web.Controllers
{
	public class MatchesManagementController : ControllerBase
	{
		private readonly IMatchesService _matchesService;

		public MatchesManagementController(IMatchesService matchesService)
		{
			_matchesService = matchesService;	
		}


		[HttpPost("Matches")]
		public async Task<IActionResult> AddMatches(MatchesDto matchesDto)
		{
			var matches = await _matchesService.AddMatchesAsync(matchesDto);
			return Ok(matches);
		}

		[HttpPut("UpdateMatches/{id}")]
		public async Task<IActionResult> UpdateMatches(int id, MatchesDto matchesDto)
		{
			var matches = await _matchesService.UpdateMatchesAsync(id, matchesDto);
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
