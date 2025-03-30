using Microsoft.AspNetCore.Mvc;
using Sport_Web.DTO;

namespace Sport_Web.Abstraction
{
	public interface IcontentService
	{
		Task<List<TeamResponseDto>> GetAllTeamsAsync();
		Task <List< TabContentDto>> GetTabContentAsync( int CategorySectionId);
		Task<HomeResponseDto> AddHomeAsync(HomeDto homeDto );

		Task<TeamResponseDto> AddTeamAsync( TeamDto team);
	}
}


























