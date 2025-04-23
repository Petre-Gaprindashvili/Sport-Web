using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sport_Web.Abstraction;
using Sport_Web.DTO;

namespace Sport_Web.Controllers
{
	[ApiController]
	[Route("api/admin/players")]
	[Authorize(Roles = "Admin")]

	public class PlayerManagementController : ControllerBase
	{
		private readonly IPlayerService _playerService;

		public PlayerManagementController(IPlayerService playerService)
		{
			_playerService = playerService;
		}



		[HttpPost("AddPlayer")]
		public async Task<IActionResult> AddPlayer(PlayerDto playerDto)
		{
			var players = await _playerService.AddPlayerAsync(playerDto);
			return Ok(players);
		}
		[HttpGet("GetPlayer{id}")]
		public async Task<ActionResult<PlayersResponseDto>> GetPlayerById(int id)
		{
			var player = await _playerService.GetPlayerByIdAsync(id);
			return Ok(player);
		}

		[HttpPut("updatePlayer/{id}")]
		public async Task<ActionResult<PlayersResponseDto>> UpdatePlayer(int id, PlayerDto playerDto)
		{
			var updatedPlayer = await _playerService.UpdatePlayerAsync(id, playerDto);
			return Ok(updatedPlayer);
		}

		[HttpDelete("deletePlayer/{id}")]
		public async Task<ActionResult> DeletePlayer(int id)
		{
			var result = await _playerService.DeletePlayerAsync(id);
			return Ok(result);
		}
	}

}
