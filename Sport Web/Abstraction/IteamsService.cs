using Sport_Web.DTO;

namespace Sport_Web.Abstraction
{
	public interface IteamsService
	{
		Task<List<TeamResponseDto>> GetAllTeamByCategoryIdAsync(int CategoryId);
		Task<TeamResponseDto> GetSingleTeamByIdAsync(int CategoryId);

		// Admin-specific methods:
		Task<TeamResponseDto> AddTeamAsync(TeamDto team);
		Task<TeamResponseDto> UpdateTeamAsync(int id, TeamDto teamDto);
		Task<bool> DeleteTeamAsync(int id);

	}
}
