using Sport_Web.DTO;

namespace Sport_Web.Abstraction
{
	public interface IMatchesService
	{
		Task<List<MatchesResponseDto>> GetMatchesByCategoryId(int CategoryId);

		Task<MatchesResponseDto> AddMatchesAsync(MatchesDto matchesDto);
		Task<MatchesResponseDto> UpdateMatchesAsync(int id, MatchesDto matchesDto);
		Task<bool> DeleteMatchesAsync(int id);

	}
}
