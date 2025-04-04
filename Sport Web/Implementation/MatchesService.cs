using Microsoft.EntityFrameworkCore;
using Sport_Web.Abstraction;
using Sport_Web.Data;
using Sport_Web.DTO;
namespace Sport_Web.Implementation
{
	public class MatchesService:IMatchesService
	{
		private readonly ApplicationDbContext _context;
		private readonly IImageUploadService _imageUploadService;

		public MatchesService(ApplicationDbContext context, IImageUploadService imageUploadService)
		{
			_context = context;
			_imageUploadService = imageUploadService;
		}

		public async Task<List<MatchesResponseDto>> GetMatchesByCategoryId(int categoryId)
		{

			var now = DateTime.Now;
			var pastMatches = await _context.Matches
		   .Include(m => m.HomeTeam)
		   .ThenInclude(t => t.Category)
			.Include(m => m.AwayTeam)
			.ThenInclude(t => t.Category)
			.Where(m => m.HomeTeam.CategoryId == categoryId || m.AwayTeam.CategoryId == categoryId)
			 .ToListAsync();

			var upcomingMatches = await _context.Matches
				.Include(m => m.HomeTeam)
				.ThenInclude(t => t.Category)
				.Include(m => m.AwayTeam)
				.ThenInclude(t => t.Category)
				.Where(m => (m.HomeTeam.CategoryId == categoryId || m.AwayTeam.CategoryId == categoryId) && m.MatchDate >= now)
				.ToListAsync();


			var allMatches = pastMatches.Concat(upcomingMatches).ToList();


			return allMatches.Select(m => new MatchesResponseDto
			{
				Id = m.Id,
				HomeTeamName = m.HomeTeam.Name,
				AwayTeamName = m.AwayTeam.Name,
				HomeScore = m.HomeScore,
				AwayScore = m.AwayScore,
				MatchDate = m.MatchDate,
				Winner = m.HomeScore > m.AwayScore ? m.HomeTeam.Name : m.AwayTeam.Name,
				CategoryId = m.HomeTeam.CategoryId,
				CategoryName = m.HomeTeam.Category.Name,
				HomeTeamLogo = m.HomeTeamlogo ?? string.Empty,
				AwayTeamLogo = m.AwayTeamlogo ?? string.Empty

			}).ToList();

		}

		public async Task<MatchesResponseDto> AddMatchesAsync(MatchesDto matchesDto)
		{

			var homeTeam = await _context.Teams.Include(t => t.Category).FirstOrDefaultAsync(t => t.Id == matchesDto.HomeTeamId);
			var awayTeam = await _context.Teams.Include(t => t.Category).FirstOrDefaultAsync(t => t.Id == matchesDto.AwayTeamId);

			if (homeTeam == null || awayTeam == null)
			{
				var response = new ResponseDto
				{
					IsSuccess = false,
					Message = "One or both teams not found."
				};
			}


			if (homeTeam.CategoryId != awayTeam.CategoryId)
			{
				var response = new ResponseDto
				{
					IsSuccess = false,
					Message = "Teams must belong to the same category"
				};
			}

			string homeTeamLogoUrl = await _imageUploadService.UploadImageAsync(matchesDto.HomeTeamLogo);
			string awayTeamLogoUrl = await _imageUploadService.UploadImageAsync(matchesDto.AwayTeamLogo);

			var match = new Sport_Web.Models.Match
			{
				HomeTeamId = matchesDto.HomeTeamId,
				AwayTeamId = matchesDto.AwayTeamId,
				HomeScore = matchesDto.HomeScore,
				AwayScore = matchesDto.AwayScore,
				MatchDate = matchesDto.MatchDate,
				HomeTeamlogo = homeTeamLogoUrl,
				AwayTeamlogo = awayTeamLogoUrl,


			};

			_context.Matches.Add(match);
			await _context.SaveChangesAsync();

			return new MatchesResponseDto
			{
				Id = match.Id,
				HomeTeamName = homeTeam.Name,
				AwayTeamName = awayTeam.Name,
				HomeScore = match.HomeScore,
				AwayScore = match.AwayScore,
				MatchDate = match.MatchDate,
				Winner = match.HomeScore > match.AwayScore ? homeTeam.Name : awayTeam.Name,
				CategoryId = homeTeam.CategoryId,
				CategoryName = homeTeam.Category.Name,
				HomeTeamLogo = homeTeamLogoUrl,
				AwayTeamLogo = awayTeamLogoUrl,
			};

		}

		public async Task<MatchesResponseDto> UpdateMatchesAsync(int id, MatchesDto matchesDto)
		{
			var matches = await _context.Matches.FirstOrDefaultAsync(match => match.Id == id);
			if (matches == null)
			{

				var response = new ResponseDto
				{
					IsSuccess = false,
					Message = "Invalid id."
				};
			}
			matches.HomeTeamId = matchesDto.HomeTeamId;
			matches.AwayTeamId = matchesDto.AwayTeamId;
			matches.HomeScore = matchesDto.HomeScore;
			matches.AwayScore = matchesDto.AwayScore;
			matches.MatchDate = matchesDto.MatchDate;
			await _context.SaveChangesAsync();
			var homeTeam = await _context.Teams.FindAsync(matches.HomeTeamId);
			var awayTeam = await _context.Teams.FindAsync(matches.AwayTeamId);

			return new MatchesResponseDto
			{
				Id = matches.Id,
				HomeTeamName = homeTeam?.Name,
				AwayTeamName = awayTeam?.Name,
				HomeScore = matches.HomeScore,
				AwayScore = matches.AwayScore,
				MatchDate = matches.MatchDate,
				Winner = matches.HomeScore > matches.AwayScore ? homeTeam?.Name : awayTeam?.Name
			};
		}

		public async Task<bool> DeleteMatchesAsync(int id)
		{
			var matches = _context.Matches.FirstOrDefault(h => h.Id == id);
			if (matches == null)
			{
				return false;
			}
			bool hasMatches = await _context.Matches.AnyAsync(m => m.HomeTeamId == id || m.AwayTeamId == id);
			if (hasMatches) return false;
			_context.Matches.Remove(matches);
			await _context.SaveChangesAsync();
			return true;
		}

	}
}
