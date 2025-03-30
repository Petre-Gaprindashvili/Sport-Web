using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sport_Web.Abstraction;
using Sport_Web.DTO;

namespace Sport_Web.Controllers
{
	[ApiController]
	[Route("api/admin/content")]
	[Authorize(Roles = "Admin")]
	public class ContentManagementController : ControllerBase
	{
		private readonly IcontentService _contentService;	

		public ContentManagementController (IcontentService contentService)
		{
			_contentService = contentService;

		}
		[HttpPost("Team")]	
		public async Task<IActionResult> AddTeam(TeamDto teamDto)
		{
			var team = await _contentService.AddTeamAsync(teamDto);
			return Ok(team);	
		}

		[HttpPost("Home")]
		public async Task<IActionResult> AddHome(HomeDto homeDto)
		{
			var team = await _contentService.AddHomeAsync(homeDto);
			return Ok(team);
		}




	}
}

