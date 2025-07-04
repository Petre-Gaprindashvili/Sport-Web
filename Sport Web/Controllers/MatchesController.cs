﻿using Microsoft.AspNetCore.Mvc;
using Sport_Web.Abstraction;
using Sport_Web.Data;
using Sport_Web.Implementation;

namespace Sport_Web.Controllers
{
	[ApiController]
	[Route("api/matches")]
	public class MatchesController : ControllerBase
	{

		private readonly IMatchesService _matchesService;
		private readonly ApplicationDbContext _context;

		public MatchesController(IMatchesService matchesService, ApplicationDbContext dbContext)
		{
			_context = dbContext;
			_matchesService = matchesService;
		}

		[HttpGet("GetAllMatches")]
		public async Task<IActionResult> GetAllMatches()
		{
			var response = await _matchesService.GetAllMatchesAsync();
			return Ok(response);

		}

		[HttpGet("GetMatches/{categoryId}")]
		public async Task<IActionResult> GetMatchesByCategory(int categoryId)
		{
			var response = await _matchesService.GetMatchesByCategoryId(categoryId);
			return Ok(response);

		}

		[HttpGet("GetMatchesByTeam/{teamId}")]
		public async Task<IActionResult> GetMatchesByTeam(int teamId)
		{
			var response = await _matchesService.GetMatchesByTeamId(teamId);
			return Ok(response);

		}

	}
}
