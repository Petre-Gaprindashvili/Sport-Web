using Sport_Web.DTO;

namespace Sport_Web.Abstraction
{
	public interface IMatchesService
	{
		Task<List<MatchesResponseDto>> GetAllMatchesAsync();
		Task<List<MatchesResponseDto>> GetMatchesByCategoryId(int CategoryId);
		Task<List<MatchesResponseDto>> GetMatchesByTeamId(int TeamId);



		Task<MatchesResponseDto> AddMatchesAsync(MatchesDto matchesDto);
		Task<MatchesResponseDto> UpdateMatchesAsync(int id, UpdateMatchesDto updateMatchesDto);
		Task<bool> DeleteMatchesAsync(int id);

	}
}

