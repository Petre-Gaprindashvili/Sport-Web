using Sport_Web.DTO;

namespace Sport_Web.Abstraction
{
	public interface IPlayerService
	{
		Task<List<PlayersResponseDto>> GetPlayersByTeamIdAsync(int teamId);

		// Admin-specific methods:
		Task<PlayersResponseDto> AddPlayerAsync(PlayerDto playerDto);
		Task<PlayersResponseDto> GetPlayerByIdAsync(int playerId);
		Task<PlayersResponseDto> UpdatePlayerAsync(int playerId, PlayerDto playerDto);
		Task<bool> DeletePlayerAsync(int playerId);

	}
}

