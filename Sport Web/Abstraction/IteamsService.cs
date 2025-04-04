using Sport_Web.DTO;

namespace Sport_Web.Abstraction
{
	public interface IteamsService
	{
		Task<List<TeamResponseDto>> GetTeamByCategoryIdAsync(int CategoryId);

		Task<TeamResponseDto> AddTeamAsync(TeamDto team);
		Task<TeamResponseDto> UpdateTeamAsync(int id, TeamDto teamDto);
		Task<bool> DeleteTeamAsync(int id);

	}
}
