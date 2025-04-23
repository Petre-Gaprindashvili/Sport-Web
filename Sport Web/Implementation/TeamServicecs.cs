using Microsoft.EntityFrameworkCore;
using Sport_Web.Abstraction;
using Sport_Web.Data;
using Sport_Web.DTO;
using Sport_Web.Models;

namespace Sport_Web.Implementation
{
	public class TeamServicecs :IteamsService
	{
		private readonly ApplicationDbContext _context;
		private readonly IImageUploadService _imageUploadService;

		public TeamServicecs(ApplicationDbContext context, IImageUploadService imageUploadService)
		{
			_context = context;
			_imageUploadService = imageUploadService;
		}

		public async Task<List<TeamResponseDto>> GetAllTeamByCategoryIdAsync(int categoryId)
		{

			var teams = await _context.Teams
		.Where(t => t.CategoryId == categoryId)
		.Select(t => new TeamResponseDto
		{
			Id = t.Id,
			TeamName = t.Name,
			LogoUrl = t.LogoUrl,
			CategoryId = t.CategoryId,

		}).ToListAsync();


			return teams;
		}

		public async Task<TeamResponseDto> GetSingleTeamByIdAsync(int id)
		{
			var team = await _context.Teams.Where(t => t.Id == id).
				Select(t => new TeamResponseDto
				{
					Id = t.Id,
					TeamName = t.Name,
					LogoUrl = t.LogoUrl,
					CategoryId = t.CategoryId,

				}).FirstOrDefaultAsync();

			return team;
		}



		public async Task<TeamResponseDto> AddTeamAsync(TeamDto teamDto)
		{
			var sectionContent = await _context.categorySections
			.FirstOrDefaultAsync(c => c.CategoryId == teamDto.CategoryId);
			if (sectionContent == null) return null;

			string imageUrl = teamDto.LogoUrl;	

			var team = new Team
			{

				Name = teamDto.TeamName,
				CategoryId = sectionContent.CategoryId,
				LogoUrl = imageUrl,
			};
			_context.Teams.Add(team);
			await _context.SaveChangesAsync();

			return new TeamResponseDto
			{

				Id = team.Id,
				TeamName = team.Name,
				//SectionName = team.SectionContent.ContentType,
				LogoUrl = imageUrl,
				CategoryId = team.CategoryId,


			};
		}

		public async Task<TeamResponseDto> UpdateTeamAsync(int id, TeamDto teamDto)
		{
			var teams = await _context.Teams.FirstOrDefaultAsync(c => c.Id == id);
			if (teams == null)
			{

				var response = new ResponseDto
				{
					IsSuccess = false,
					Message = "Invalid Id"
				};
			}

			teams.Name = teamDto.TeamName;
			//teams.LogoUrl = teamDto.LogoUrl;
			await _context.SaveChangesAsync();
			return new TeamResponseDto
			{
				Id = teams.Id,
				TeamName = teams.Name,

				LogoUrl = teams.LogoUrl,
				CategoryId = teams.CategoryId,

			};
		}

		public async Task<bool> DeleteTeamAsync(int id)
		{
			var teams = _context.Teams.FirstOrDefault(h => h.Id == id);
			if (teams == null)
			{
				var response = new ResponseDto
				{
					IsSuccess = false,
					Message = "Invalid Id"
				};
			}

			bool hasMatches = await _context.Matches.AnyAsync(m => m.HomeTeamId == id || m.AwayTeamId == id);
			if (hasMatches) return false;

			_context.Teams.Remove(teams);
			await _context.SaveChangesAsync();
			return true;
		}

	}
}
