using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Sport_Web.Abstraction;
using Sport_Web.DTO;
using Sport_Web.Implementation;

namespace Sport_Web.Controllers
{
	public class PlayerController
	{
		private readonly IPlayerService _playerService;	

		public PlayerController(IPlayerService playerService)
		{
			_playerService = playerService;	
		}

		[HttpGet("GetPlayer/{teamId}")]
		public async Task<ActionResult<List<PlayersResponseDto>>> GetPlayersByTeam(int teamId)
		{
			var players = await _playerService.GetPlayersByTeamIdAsync(teamId);
			return players;
		}


	}
}

