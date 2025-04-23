using Microsoft.AspNetCore.Mvc;
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
			var matches = await _context.Matches
				.Include(m => m.HomeTeam)
				.Include(m => m.AwayTeam)
				.Include(m => m.HomeTeam.Category)
				.Include(m => m.AwayTeam.Category)
				.Where(m => m.HomeTeam.CategoryId == categoryId || m.AwayTeam.CategoryId == categoryId)
				.ToListAsync();

			// Split matches into upcoming and past
			var upcomingMatches = matches.Where(m => m.MatchDate > DateTime.Now).ToList();
			var pastMatches = matches.Where(m => m.MatchDate <= DateTime.Now).ToList();

			// Remove duplicates by match Id
			var distinctUpcomingMatches = upcomingMatches
				.GroupBy(m => m.Id)
				.Select(g => g.First())
				.ToList();

			var distinctPastMatches = pastMatches
				.GroupBy(m => m.Id)
				.Select(g => g.First())
				.ToList();

			// Combine both upcoming and past matches
			var combinedMatches = distinctUpcomingMatches.Concat(distinctPastMatches).ToList();
			if (!combinedMatches.Any())
			{
				return new List<MatchesResponseDto>
		   {
		   new MatchesResponseDto
		  {
			IsSuccess = false,
			Message = "No matches available."
		  }
		};
			}

			// Map to DTO and handle winner logic only for past matches
			return combinedMatches.Select(match => new MatchesResponseDto
			{
				Id = match.Id,
				HomeTeamName = match.HomeTeam.Name,
				AwayTeamName = match.AwayTeam.Name,
				HomeScore = match.HomeScore,
				AwayScore = match.AwayScore,
				MatchDate = match.MatchDate,
				Winner = match.MatchDate <= DateTime.Now
					? (match.HomeScore > match.AwayScore
						? match.HomeTeam.Name
						: (match.HomeScore < match.AwayScore
							? match.AwayTeam.Name
							: null))
					: null,
				CategoryId = match.HomeTeam.CategoryId,
				HomeTeamLogo = match.HomeTeamlogo,
				AwayTeamLogo = match.AwayTeamlogo,
				IsSuccess = true,
				Message = "Matches retrieved successfully"
			}).ToList();
		}

		public async Task<List<MatchesResponseDto>> GetMatchesByTeamId(int teamId)
		{
			var matches = await _context.Matches
				.Include(m => m.HomeTeam)
				.Include(m => m.AwayTeam)
				.Include(m => m.HomeTeam.Category)
				.Include(m => m.AwayTeam.Category)
				.Where(m => m.HomeTeamId == teamId || m.AwayTeamId == teamId)
				.ToListAsync();

			// Split into upcoming and past
			var upcomingMatches = matches.Where(m => m.MatchDate > DateTime.Now).ToList();
			var pastMatches = matches.Where(m => m.MatchDate <= DateTime.Now).ToList();

			// Remove duplicates by match Id
			var distinctUpcomingMatches = upcomingMatches
				.GroupBy(m => m.Id)
				.Select(g => g.First())
				.ToList();

			var distinctPastMatches = pastMatches
				.GroupBy(m => m.Id)
				.Select(g => g.First())
				.ToList();

			// Combine both lists
			var combinedMatches = distinctUpcomingMatches.Concat(distinctPastMatches).ToList();
			if (!combinedMatches.Any())
			{
				return new List<MatchesResponseDto>
	       {
		   new MatchesResponseDto
		  {
			IsSuccess = false,
			Message = "No matches available."
		  }
	    };
			}

			// Map to DTO
			return combinedMatches.Select(match => new MatchesResponseDto
			{
				Id = match.Id,
				HomeTeamName = match.HomeTeam?.Name,
				AwayTeamName = match.AwayTeam?.Name,
				HomeScore = match.HomeScore,
				AwayScore = match.AwayScore,
				MatchDate = match.MatchDate,
				Winner = match.MatchDate <= DateTime.Now
					? (match.HomeScore > match.AwayScore
						? match.HomeTeam?.Name
						: (match.HomeScore < match.AwayScore
							? match.AwayTeam?.Name
							: null))
					: null,
				CategoryId = match.HomeTeam.CategoryId,
				HomeTeamLogo = match.HomeTeamlogo,
				AwayTeamLogo = match.AwayTeamlogo,
				IsSuccess = true,
				Message = "Matches retrieved successfully"
			}).ToList();
		}



		public async Task<MatchesResponseDto> AddMatchesAsync(MatchesDto matchesDto)
		{
			var homeTeam = await _context.Teams.Include(t => t.Category).FirstOrDefaultAsync(t => t.Id == matchesDto.HomeTeamId);
			var awayTeam = await _context.Teams.Include(t => t.Category).FirstOrDefaultAsync(t => t.Id == matchesDto.AwayTeamId);

			if (homeTeam == null || awayTeam == null)
			{
				var response =  new ResponseDto
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
					Message = "Teams must belong to the same category."
				};
			}

			string homeTeamLogoUrl = matchesDto.HomeTeamLogo;
			string awayTeamLogoUrl = matchesDto.AwayTeamLogo;
			bool isFutureMatch = matchesDto.MatchDate > DateTime.UtcNow.Date;
			bool isTodayMatch = matchesDto.MatchDate.Date == DateTime.UtcNow.Date;
			bool isPastMatch = matchesDto.MatchDate.Date < DateTime.UtcNow.Date;
			bool hasAnyScore = matchesDto.HomeScore.HasValue || matchesDto.AwayScore.HasValue;
			bool hasBothScores = matchesDto.HomeScore.HasValue && matchesDto.AwayScore.HasValue;



			if (isFutureMatch && hasAnyScore)
			{
				return new MatchesResponseDto
				{
					IsSuccess = false,
					Message = "You cannot set scores for a future match."
				};
			}

			if(isPastMatch && !hasBothScores)
			{
				return new MatchesResponseDto
				{
					IsSuccess = false,
					Message = "Past matches must include both scores."
				};
			}

				if (isTodayMatch && hasAnyScore && !hasBothScores)
	{
		return new MatchesResponseDto
		{
			IsSuccess = false,
			Message = "Please enter both scores or leave them empty for today's match."
		};
	}

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

			// Conditionally calculate winner only for past matches with scores
			string winner = null;
            if (hasBothScores)
			{
				if (match.HomeScore > match.AwayScore)
					winner = homeTeam.Name;
				else if (match.HomeScore < match.AwayScore)
					winner = awayTeam.Name;
				else
					winner = "Draw";

			}

			return new MatchesResponseDto
			{
				Id = match.Id,
				HomeTeamName = homeTeam.Name,
				AwayTeamName = awayTeam.Name,
				HomeScore = match.HomeScore,
				AwayScore = match.AwayScore,
				MatchDate = match.MatchDate,
				Winner = winner,
				CategoryId = homeTeam.CategoryId,
				HomeTeamLogo = homeTeamLogoUrl,
				AwayTeamLogo = awayTeamLogoUrl,
				IsSuccess = true,
				Message = "Match added successfully"
			};
		}


		//public async Task<MatchesResponseDto> AddMatchesAsync(MatchesDto matchesDto)
		//{

		//	var homeTeam = await _context.Teams.Include(t => t.Category).FirstOrDefaultAsync(t => t.Id == matchesDto.HomeTeamId);
		//	var awayTeam = await _context.Teams.Include(t => t.Category).FirstOrDefaultAsync(t => t.Id == matchesDto.AwayTeamId);

		//	if (homeTeam == null || awayTeam == null)
		//	{
		//		var response = new ResponseDto
		//		{
		//			IsSuccess = false,
		//			Message = "One or both teams not found."
		//		};
		//	}


		//	if (homeTeam.CategoryId != awayTeam.CategoryId)
		//	{
		//		var response = new ResponseDto
		//		{
		//			IsSuccess = false,
		//			Message = "Teams must belong to the same category"
		//		};
		//	}

		//	string homeTeamLogoUrl = await _imageUploadService.UploadImageAsync(matchesDto.HomeTeamLogo);
		//	string awayTeamLogoUrl = await _imageUploadService.UploadImageAsync(matchesDto.AwayTeamLogo);

		//	var match = new Sport_Web.Models.Match
		//	{
		//		HomeTeamId = matchesDto.HomeTeamId,
		//		AwayTeamId = matchesDto.AwayTeamId,
		//		HomeScore = matchesDto.HomeScore,
		//		AwayScore = matchesDto.AwayScore,
		//		MatchDate = matchesDto.MatchDate,
		//		HomeTeamlogo = homeTeamLogoUrl,
		//		AwayTeamlogo = awayTeamLogoUrl,


		//	};

		//	_context.Matches.Add(match);
		//	await _context.SaveChangesAsync();

		//	return new MatchesResponseDto
		//	{
		//		Id = match.Id,
		//		HomeTeamName = homeTeam.Name,
		//		AwayTeamName = awayTeam.Name,
		//		HomeScore = match.HomeScore,
		//		AwayScore = match.AwayScore,
		//		MatchDate = match.MatchDate,
		//		Winner = match.HomeScore > match.AwayScore ? homeTeam.Name : awayTeam.Name,
		//		CategoryId = homeTeam.CategoryId,
		//		CategoryName = homeTeam.Category.Name,
		//		HomeTeamLogo = homeTeamLogoUrl,
		//		AwayTeamLogo = awayTeamLogoUrl,
		//	};

		//}

		public async Task<MatchesResponseDto> UpdateMatchesAsync(int id, UpdateMatchesDto updateMatchesDto)
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


			var now = DateTime.UtcNow;

			if (matches.MatchDate.Date > now.Date)
			{
				return new MatchesResponseDto
				{
					IsSuccess = false,
					Message = "You can only update a match after it has ended."
				};
			}


			matches.HomeTeamId = updateMatchesDto.HomeTeamId;
			matches.AwayTeamId = updateMatchesDto.AwayTeamId;
			matches.HomeScore = updateMatchesDto.HomeScore;
			matches.AwayScore = updateMatchesDto.AwayScore;
			//matches.MatchDate = matchesDto.MatchDate;
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
