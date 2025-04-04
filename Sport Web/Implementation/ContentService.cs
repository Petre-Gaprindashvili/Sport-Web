//using Microsoft.AspNetCore.Components.Sections;
//using Microsoft.AspNetCore.Http.HttpResults;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;
//using Microsoft.Identity.Client.TelemetryCore.TelemetryClient;
//using Sport_Web.Abstraction;
//using Sport_Web.Data;
//using Sport_Web.DTO;
//using Sport_Web.Models;
//using System.Net.Sockets;
//using System.Text.RegularExpressions;
//using static System.Collections.Specialized.BitVector32;

//namespace Sport_Web.Implementation
//{
//	public class ContentService : IcontentService
//	{
//		private readonly ApplicationDbContext _context;
//		private readonly IImageUploadService _imageUploadService;

//		public ContentService(ApplicationDbContext context, IImageUploadService imageUploadService)
//		{
//			_context = context;
//			_imageUploadService = imageUploadService;
//		}






//		//public async Task<HomeResponseDto> AddHomeAsync(HomeDto homeDto)
//		//{
//		//	var sectionContent = await _context.SectionContents
//		//	.FirstOrDefaultAsync(c => c.Id == homeDto.CategorySectionId);
//		//	string imageUrl = await _imageUploadService.UploadImageAsync(homeDto.LogoUrl);

//		//	var home = new Home
//		//	{

//		//		Title = homeDto.Tittle,
//		//		SectionContentId = sectionContent.Id,
//		//		ImageUrl = imageUrl,
//		//		Description = homeDto.Description,

//		//	};
//		//	_context.Homes.Add(home);
//		//	await _context.SaveChangesAsync();

//		//	return new HomeResponseDto
//		//	{

//		//		Id = home.Id,
//		//		Tittle = home.Title,
//		//		Description = home.Description,
//		//		LogoUrl = imageUrl,
//		//		CategoryId = home.SectionContent.CategorySectionId,

//		//	};

//		//}

//		//public async Task<List<HomeResponseDto>> GetHomeByCategoryIdAsync(int categoryId)
//		//{

//		//	var home = await _context.Homes
//		//.Where(t => t.SectionContentId == categoryId)
//		//.Select(t => new HomeResponseDto
//		//{
//		//	Id = t.Id,
//		//	Tittle = t.Title,
//		//	Description = t.Description,
//		//	LogoUrl = t.ImageUrl,
//		//	CategoryId = t.SectionContentId,


//		//}).ToListAsync();


//		//	return home;
//		//}

//		//public async Task<HomeResponseDto> UpdateHomeAsync(int id, HomeDto homeDto)
//		//{
//		//	var home = await _context.Homes.FirstOrDefaultAsync(c => c.Id == id);
//		//	if (home == null)
//		//	{
//		//		var response = new ResponseDto
//		//		{
//		//			IsSuccess = false,
//		//			Message = "Invalid Id"
//		//		};
//		//	}
//		//	home.Title = homeDto.Tittle;
//		//	//home.ImageUrl = home.LogoUrl;
//		//	home.Description = homeDto.Description;
//		//	await _context.SaveChangesAsync();
//		//	return new HomeResponseDto
//		//	{
//		//		Id = home.Id,
//		//		Tittle = home.Title,
//		//		LogoUrl = home.ImageUrl,
//		//		Description = home.Description,
//		//		CategoryId = home.SectionContent?.CategorySectionId ?? 0

//		//	};
//		//}

//		//public async Task<bool> DeleteHomeAsync(int id)
//		//{
//		//	var home = _context.Homes.FirstOrDefault(h => h.Id == id);
//		//	if (home == null)
//		//	{
//		//		var response = new ResponseDto
//		//		{
//		//			IsSuccess = false,
//		//			Message = "Invalid Id"
//		//		};
//		//	}
//		//	_context.Homes.Remove(home);
//		//	await _context.SaveChangesAsync();
//		//	return true;
//		//}


//		//public async Task<TeamResponseDto> AddTeamAsync(TeamDto teamDto)
//		//{
//		//	var sectionContent = await _context.SectionContents
//		//	.FirstOrDefaultAsync(c => c.Id == teamDto.CategorySectionId);
//		//	if (sectionContent == null) return null;

//		//	string imageUrl = await _imageUploadService.UploadImageAsync(teamDto.LogoUrl);

//		//	var team = new Team
//		//	{

//		//		Name = teamDto.TeamName,
//		//		CategoryId = sectionContent.Id,
//		//		LogoUrl = imageUrl,
//		//	};
//		//	_context.Teams.Add(team);
//		//	await _context.SaveChangesAsync();

//		//	return new TeamResponseDto
//		//	{

//		//		Id = team.Id,
//		//		TeamName = team.Name,
//		//		//SectionName = team.SectionContent.ContentType,
//		//		LogoUrl = imageUrl,
//		//		CategoryId = team.CategoryId,


//		//	};
//		//}

//		//public async Task<List<TeamResponseDto>> GetTeamByCategoryIdAsync(int categoryId)
//		//{

//		//	var teams = await _context.Teams
//		//.Where(t => t.CategoryId == categoryId)
//		//.Select(t => new TeamResponseDto
//		//{
//		//	Id = t.Id,
//		//	TeamName = t.Name,
//		//	LogoUrl = t.LogoUrl,
//		//	CategoryId = t.CategoryId,

//		//}).ToListAsync();


//		//	return teams;
//		//}

//		//public async Task<TeamResponseDto> UpdateTeamAsync(int id, TeamDto teamDto)
//		//{
//		//	var teams = await _context.Teams.FirstOrDefaultAsync(c => c.Id == id);
//		//	if (teams == null)
//		//	{

//		//		var response = new ResponseDto
//		//		{
//		//			IsSuccess = false,
//		//			Message = "Invalid Id"
//		//		};
//		//	}

//		//	teams.Name = teamDto.TeamName;
//		//	//teams.LogoUrl = teamDto.LogoUrl;
//		//	await _context.SaveChangesAsync();
//		//	return new TeamResponseDto
//		//	{
//		//		Id = teams.Id,
//		//		TeamName = teams.Name,

//		//		LogoUrl = teams.LogoUrl,
//		//		CategoryId = teams.CategoryId,

//		//	};
//		//}

//		//public async Task<bool> DeleteTeamAsync(int id)
//		//{
//		//	var teams = _context.Teams.FirstOrDefault(h => h.Id == id);
//		//	if (teams == null)
//		//	{
//		//		var response = new ResponseDto
//		//		{
//		//			IsSuccess = false,
//		//			Message = "Invalid Id"
//		//		};
//		//	}

//		//	bool hasMatches = await _context.Matches.AnyAsync(m => m.HomeTeamId == id || m.AwayTeamId == id);
//		//	if (hasMatches) return false;

//		//	_context.Teams.Remove(teams);
//		//	await _context.SaveChangesAsync();
//		//	return true;
//		//}

//		public async Task<MatchesResponseDto> AddMatchesAsync(MatchesDto matchesDto)
//		{

//			var homeTeam = await _context.Teams.Include(t => t.Category).FirstOrDefaultAsync(t => t.Id == matchesDto.HomeTeamId);
//			var awayTeam = await _context.Teams.Include(t => t.Category).FirstOrDefaultAsync(t => t.Id == matchesDto.AwayTeamId);

//			if (homeTeam == null || awayTeam == null)
//			{
//				var response = new ResponseDto
//				{
//					IsSuccess = false,
//					Message = "One or both teams not found."
//				};
//			}


//			if (homeTeam.CategoryId != awayTeam.CategoryId)
//			{
//				var response = new ResponseDto
//				{
//					IsSuccess = false,
//					Message = "Teams must belong to the same category"
//				};
//			}

//			string homeTeamLogoUrl = await _imageUploadService.UploadImageAsync(matchesDto.HomeTeamLogo);
//			string awayTeamLogoUrl = await _imageUploadService.UploadImageAsync(matchesDto.AwayTeamLogo);

//			var match = new Sport_Web.Models.Match
//			{
//				HomeTeamId = matchesDto.HomeTeamId,
//				AwayTeamId = matchesDto.AwayTeamId,
//				HomeScore = matchesDto.HomeScore,
//				AwayScore = matchesDto.AwayScore,
//				MatchDate = matchesDto.MatchDate,
//				HomeTeamlogo = homeTeamLogoUrl,
//				AwayTeamlogo = awayTeamLogoUrl,


//			};

//			_context.Matches.Add(match);
//			await _context.SaveChangesAsync();

//			return new MatchesResponseDto
//			{
//				Id = match.Id,
//				HomeTeamName = homeTeam.Name,
//				AwayTeamName = awayTeam.Name,
//				HomeScore = match.HomeScore,
//				AwayScore = match.AwayScore,
//				MatchDate = match.MatchDate,
//				Winner = match.HomeScore > match.AwayScore ? homeTeam.Name : awayTeam.Name,
//				CategoryId = homeTeam.CategoryId,
//				CategoryName = homeTeam.Category.Name,
//				HomeTeamLogo = homeTeamLogoUrl,
//				AwayTeamLogo = awayTeamLogoUrl,
//			};

//		}

//		public async Task<MatchesResponseDto> UpdateMatchesAsync(int id, MatchesDto matchesDto)
//		{
//			var matches = await _context.Matches.FirstOrDefaultAsync(match => match.Id == id);
//			if (matches == null)
//			{

//				var response = new ResponseDto
//				{
//					IsSuccess = false,
//					Message = "Invalid id."
//				};
//			}
//			matches.HomeTeamId = matchesDto.HomeTeamId;
//			matches.AwayTeamId = matchesDto.AwayTeamId;
//			matches.HomeScore = matchesDto.HomeScore;
//			matches.AwayScore = matchesDto.AwayScore;
//			matches.MatchDate = matchesDto.MatchDate;
//			await _context.SaveChangesAsync();
//			var homeTeam = await _context.Teams.FindAsync(matches.HomeTeamId);
//			var awayTeam = await _context.Teams.FindAsync(matches.AwayTeamId);

//			return new MatchesResponseDto
//			{
//				Id = matches.Id,
//				HomeTeamName = homeTeam?.Name,
//				AwayTeamName = awayTeam?.Name,
//				HomeScore = matches.HomeScore,
//				AwayScore = matches.AwayScore,
//				MatchDate = matches.MatchDate,
//				Winner = matches.HomeScore > matches.AwayScore ? homeTeam?.Name : awayTeam?.Name
//			};
//		}

//		public async Task<bool> DeleteMatchesAsync(int id)
//		{
//			var matches = _context.Matches.FirstOrDefault(h => h.Id == id);
//			if (matches == null)
//			{
//				return false;
//			}
//			bool hasMatches = await _context.Matches.AnyAsync(m => m.HomeTeamId == id || m.AwayTeamId == id);
//			if (hasMatches) return false;
//			_context.Matches.Remove(matches);
//			await _context.SaveChangesAsync();
//			return true;
//		}

//		public async Task<List<MatchesResponseDto>> GetMatchesByCategoryId(int categoryId)
//		{

//			var now = DateTime.Now;
//			var pastMatches = await _context.Matches
//	       .Include(m => m.HomeTeam)
//	       .ThenInclude(t => t.Category)
//        	.Include(m => m.AwayTeam)
//	        .ThenInclude(t => t.Category)
//	        .Where(m => m.HomeTeam.CategoryId == categoryId || m.AwayTeam.CategoryId == categoryId)
//         	.ToListAsync();

//			var upcomingMatches = await _context.Matches
//				.Include(m => m.HomeTeam)
//				.ThenInclude(t => t.Category)
//				.Include(m => m.AwayTeam)
//				.ThenInclude(t => t.Category)
//				.Where(m => (m.HomeTeam.CategoryId == categoryId || m.AwayTeam.CategoryId == categoryId) && m.MatchDate >= now)
//				.ToListAsync();


//			var allMatches = pastMatches.Concat(upcomingMatches).ToList();


//			return allMatches.Select(m => new MatchesResponseDto
//			{
//				Id = m.Id,
//				HomeTeamName = m.HomeTeam.Name,
//				AwayTeamName = m.AwayTeam.Name,
//				HomeScore = m.HomeScore,
//				AwayScore = m.AwayScore,
//				MatchDate = m.MatchDate,
//				Winner = m.HomeScore > m.AwayScore ? m.HomeTeam.Name : m.AwayTeam.Name,
//				CategoryId = m.HomeTeam.CategoryId,
//				CategoryName = m.HomeTeam.Category.Name,
//				HomeTeamLogo = m.HomeTeamlogo ?? string.Empty,
//				AwayTeamLogo = m.AwayTeamlogo ?? string.Empty

//			}).ToList();

//		}

//		public async Task<NewsResponseDto> AddNewsAsync(NewsDto newsDto)
//		{
//			var sectionContent = await _context.SectionContents.FirstOrDefaultAsync(c => c.Id == newsDto.CategoryId);
//			if (sectionContent == null)
//			{
//				var response = new ResponseDto
//				{
//					IsSuccess = false,
//					Message = "Invalid id."
//				};
//			}

//			string image = await _imageUploadService.UploadImageAsync(newsDto.ImageUrl);

//			var news = new Articles
//			{

		
//				Title = newsDto.Title,
//				Content = newsDto.Content,
//				ImageUrl = image,
//				PublishedDate = newsDto.PublishedDate,
//				CategoryId = newsDto.CategoryId,
//			};
//			_context.Articles.Add(news);	
//			await _context.SaveChangesAsync();

//			return new NewsResponseDto
//			{
//				Id = news.Id,
//				Title = news.Title,
//				Content = news.Content,
//				PublishedDate = news.PublishedDate,
//				CategoryId = news.CategoryId,
//				ImageUrl = news.ImageUrl,

//			};
			
//		}

		

//		public async Task<NewsResponseDto> UpdateNewsAsync(int id, NewsDto newsDto)
//		{
//			var news = await _context.Articles.FirstOrDefaultAsync(c => c.Id == id);
//			if (news == null)
//			{
//				var response = new ResponseDto
//				{
//					IsSuccess = false,
//					Message = "Invalid Id"
//				};
//			}

//			string updatedPhotoUrl = news.ImageUrl;

//			updatedPhotoUrl = await _imageUploadService.UploadImageAsync(newsDto.ImageUrl);


//			news.Title = newsDto.Title;
//			news.Content = newsDto.Content;
//			news.PublishedDate = newsDto.PublishedDate;
//			news.ImageUrl = updatedPhotoUrl;
//			await _context.SaveChangesAsync();
//			return new NewsResponseDto
//			{
//				Id = news.Id,
//				Title = news.Title,
//				Content = news.Content,
//				ImageUrl = news.ImageUrl,

//			};
//		}

//		public async Task<bool> DeleteNewsAsync(int id)
//		{
//			var news =  _context.Articles.FirstOrDefault(h => h.Id == id);
//			if (news == null)
//			{
//				var response = new ResponseDto
//				{
//					IsSuccess = false,
//					Message = "Invalid Id"
//				};
//			}
//			_context.Articles.Remove(news);	
//			await _context.SaveChangesAsync();	
//			return true;	
//		}

//		public async Task<PlayersResponseDto> AddPlayerAsync(PlayerDto playerDto)
//		{
//			var teamExists = await _context.Teams.FirstOrDefaultAsync(t => t.Id == playerDto.TeamId);
//			if (teamExists == null)
//			{

//				var response = new ResponseDto
//				{
//					IsSuccess = false,
//					Message = "Invalid Id"
//				};

//			}
//			var photoUrl = await _imageUploadService.UploadImageAsync(playerDto.PhotoUrl);
//			var player = new Player
//			{
//				Name = playerDto.Name,
//				TeamId = playerDto.TeamId,
//				Age = playerDto.Age,
//				Position = playerDto.Position,
//				Height = playerDto.Height,
//				Weight = playerDto.Weight,
//				Country = playerDto.Country,
//				PhotoUrl = photoUrl
//			};
//			_context.Players.Add(player);
//			await _context.SaveChangesAsync();

//			return new PlayersResponseDto
//			{
//				Id = player.Id,
//				Name = player.Name,
//				TeamId = player.TeamId,
//				Age = player.Age,
//				Position = player.Position,
//				Height = player.Height,
//				Weight = player.Weight,
//				Country = player.Country,
//				PhotoUrl = photoUrl
//			};
//		}

//		public async Task<List<PlayersResponseDto>> GetPlayersByTeamIdAsync(int teamId)
//		{
//			var players = await _context.Players.Where(p => p.TeamId == teamId)
//							.Select(p => new PlayersResponseDto
//							{
//								Id = p.Id,
//								Name = p.Name,
//								TeamId = p.TeamId,
//								Age = p.Age,
//								Position = p.Position,
//								Height = p.Height,
//								Weight = p.Weight,
//								Country = p.Country,
//								PhotoUrl = p.PhotoUrl
//							}).ToListAsync();

//			return players;
//		}

//		public async Task<PlayersResponseDto> GetPlayerByIdAsync(int playerId)
//		{
//			var player = await _context.Players.FirstOrDefaultAsync(p => p.Id == playerId);

//			if (player == null)
//			{
//				var response = new ResponseDto
//				{
//					IsSuccess = false,
//					Message = "Invalid Id"
//				};
//			}

//			return new PlayersResponseDto
//			{
//				Id = player.Id,
//				Name = player.Name,
//				TeamId = player.TeamId,
//				Age = player.Age,
//				Position = player.Position,
//				Height = player.Height,
//				Weight = player.Weight,
//				Country = player.Country,
//				PhotoUrl = player.PhotoUrl
//			};
//		}

//		public async Task<PlayersResponseDto> UpdatePlayerAsync(int playerId, PlayerDto playerDto)
//		{
//			var player = await _context.Players.FindAsync(playerId);
//			if (player == null) throw new ArgumentException("Player not found.");

//			var team = await _context.Teams.FirstOrDefaultAsync(t => t.Id == playerDto.TeamId);
//			if (team == null) throw new ArgumentException("Invalid TeamId. Team does not exist.");

//			string updatedPhotoUrl = player.PhotoUrl;

//			updatedPhotoUrl = await _imageUploadService.UploadImageAsync(playerDto.PhotoUrl);


//			player.Name = playerDto.Name;
//			player.TeamId = playerDto.TeamId;
//			player.Age = playerDto.Age;
//			player.Position = playerDto.Position;
//			player.Height = playerDto.Height;
//			player.Weight = playerDto.Weight;
//			player.Country = playerDto.Country;
//			player.PhotoUrl = updatedPhotoUrl;

//			await _context.SaveChangesAsync();

//			return new PlayersResponseDto
//			{
//				TeamId = player.TeamId,
//				Age = player.Age,
//				Id = player.Id,
//				Name = player.Name,
//				Position = player.Position,
//				Height = player.Height,
//				Weight = player.Weight,
//				Country = player.Country,
//				PhotoUrl = updatedPhotoUrl
//			};
//		}

//		public async Task<bool> DeletePlayerAsync(int playerId)
//		{
//			var player = await _context.Players.FindAsync(playerId);
//			if (player == null) return false;

//			_context.Players.Remove(player);
//			await _context.SaveChangesAsync();
//			return true;
//		}
	
//		public async Task<ProductResponseDtocs> AddProductAsync(ProductDtocs productDto)
//		{
//			var teamExists = await _context.Teams.FirstOrDefaultAsync(t => t.Id == productDto.TeamId);
//			if (teamExists == null)
//			{
//				var response = new ResponseDto
//				{
//					IsSuccess = false,
//					Message = "Invalid Id"
//				};
//			}

//			var photoUrl = await _imageUploadService.UploadImageAsync(productDto.ImageUrl);

//			var product = new Product
//			{
//				Name = productDto.Name,
//				Price = productDto.Price,
//				Description = productDto.Description,
//				ImageUrl = photoUrl,
//				Stock = productDto.Stock,
//				IsAvailable = productDto.IsAvailable,
//				TeamId = productDto.TeamId,
				
//			};
//			_context.Products.Add(product);	
//			await _context.SaveChangesAsync();

//			return new ProductResponseDtocs
//			{
//				Id = product.Id,
//				Name = product.Name,
//				Price = product.Price,
//				Description = product.Description,
//				ImageUrl = product.ImageUrl,
//				Stock = product.Stock,
//				IsAvailable = product.IsAvailable,
//				TeamId = product.TeamId,
//			};

//		}


//		public async Task<List<ProductResponseDtocs>> GetProductsByTeamIdAsync(int teamId)
//		{
//			var products = await _context.Products.Where(p => p.TeamId == teamId)
//							.Select(p => new ProductResponseDtocs 
//							{
//							 Id=p.Id,	
//							 Name = p.Name,	
//							 Price = p.Price,	
//							 Description = p.Description,	
//							 ImageUrl = p.ImageUrl,	
//							 Stock = p.Stock,	
//							 IsAvailable = p.IsAvailable,	
//							 TeamId = p.TeamId,
					
//							}).ToListAsync();

//			return products;
//		}


//		public async Task<ProductResponseDtocs> GetProductByIdAsync(int productId)
//		{
//			var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
			

//			if (product == null)
//			{
//				var response = new ResponseDto
//				{
//					IsSuccess = false,
//					Message = "Invalid Id"
//				};
//			}

//			return new ProductResponseDtocs
//			{
//				Id = product.Id,
//				Name = product.Name,
//				Price = product.Price,
//				Description = product.Description,
//				ImageUrl = product.ImageUrl,
//				Stock = product.Stock,
//				IsAvailable = product.IsAvailable,
//				TeamId = product.TeamId,
//			};
//		}

//		public async Task<ProductResponseDtocs> UpdateProductAsync(int productId, ProductDtocs productDto)
//		{
//			var product = await _context.Products.FindAsync(productId);
//			if (product == null) throw new ArgumentException("Product not found.");

//			var team = await _context.Teams.FirstOrDefaultAsync(t => t.Id == productDto.TeamId);
//			if (team == null) throw new ArgumentException("Invalid TeamId. Team does not exist.");

//			string updatedPhotoUrl = product.ImageUrl;

//			updatedPhotoUrl = await _imageUploadService.UploadImageAsync(productDto.ImageUrl);


//			product.Name = productDto.Name;
//			product.TeamId = productDto.TeamId;
//			product.Price = productDto.Price;
//			product.Description = productDto.Description;
//			product.Stock = productDto.Stock;
//			product.IsAvailable = productDto.IsAvailable;
//			product.ImageUrl = updatedPhotoUrl;

//			await _context.SaveChangesAsync();

//			return new ProductResponseDtocs
//			{
//				Id = product.Id,
//				Name = product.Name,
//				Price = product.Price,
//				Description = product.Description,
//				ImageUrl = product.ImageUrl,
//				Stock = product.Stock,
//				IsAvailable = product.IsAvailable,
//				TeamId = product.TeamId,
//			};
//		}

//		public async Task<bool> DeleteProductAsync(int productId)
//		{
//			var product = await _context.Products.FindAsync(productId);
//			if (product == null) return false;

//			_context.Products.Remove(product);
//			await _context.SaveChangesAsync();
//			return true;
//		}


//	}

//}






	










	


