using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sport_Web.Abstraction;
using Sport_Web.Data;
using Sport_Web.DTO;

namespace Sport_Web.Controllers
{
	[ApiController]
	[Route("api/content")]
	public class ContentController : ControllerBase
	{
		private readonly IcontentService _contentService;
		private readonly ApplicationDbContext _context;

		public ContentController(IcontentService contentService, ApplicationDbContext dbContext  )
		{
			_contentService = contentService;	
		}
		[HttpGet]
		public async Task<IActionResult>GetAllTeams()
		{
			var getAllTeam = await _contentService.GetAllTeamsAsync();
			return Ok(getAllTeam);	
		}

		[HttpGet("{tabId}")]
		public async Task<IActionResult> GetTabContent(int tabId)
		{
			try
			{
				var responseDto = await _contentService.GetTabContentAsync(tabId);
				return Ok(responseDto);
			}
			catch (KeyNotFoundException ex)
			{
				return NotFound(ex.Message);
			}
		}


		//[HttpGet("{tabId}")]

		//public async Task<IActionResult> GetTabContentAsync(int categorySectionId)
		//{

		//	var teams = await _context.Teams
		//		   .Where(t => t.CategoryId == categorySectionId)
		//		   .ToListAsync();

		//	var teamDtos = teams.Select(t => new TeamResponseDto
		//	{
		//		//Id = t.Id,
		//		TeamName = t.Name,
		//		LogoUrl = t.LogoUrl,
		//		CategorySectionId = t.CategoryId
		//	}).ToList();


		//	return Ok(teamDtos);

	}

	}

