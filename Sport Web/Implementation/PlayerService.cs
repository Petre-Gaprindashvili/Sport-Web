using Microsoft.EntityFrameworkCore;
using Sport_Web.Abstraction;
using Sport_Web.Data;
using Sport_Web.DTO;
using Sport_Web.Models;

namespace Sport_Web.Implementation
{
	public class PlayerService:IPlayerService
	{
		private readonly ApplicationDbContext _context;

		public PlayerService(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<List<PlayersResponseDto>> GetPlayersByTeamIdAsync(int teamId)
		{
			var players = await _context.Players.Where(p => p.TeamId == teamId)
							.Select(p => new PlayersResponseDto
							{
								Id = p.Id,
								Name = p.Name,
								TeamId = p.TeamId,
								Age = p.Age,
								Position = p.Position,
								Height = p.Height,
								Weight = p.Weight,
								Country = p.Country,
								PhotoUrl = p.PhotoUrl
							}).ToListAsync();

			return players;
		}

		public async Task<PlayersResponseDto> AddPlayerAsync(PlayerDto playerDto)
		{
			var teamExists = await _context.Teams.FirstOrDefaultAsync(t => t.Id == playerDto.TeamId);
			if (teamExists == null)
			{

				var response = new ResponseDto
				{
					IsSuccess = false,
					Message = "Invalid Id"
				};

			}
			var photoUrl = playerDto.PhotoUrl;
			var player = new Player
			{
				Name = playerDto.Name,
				TeamId = playerDto.TeamId,
				Age = playerDto.Age,
				Position = playerDto.Position,
				Height = playerDto.Height,
				Weight = playerDto.Weight,
				Country = playerDto.Country,
				PhotoUrl = photoUrl
			};
			_context.Players.Add(player);
			await _context.SaveChangesAsync();

			return new PlayersResponseDto
			{
				Id = player.Id,
				Name = player.Name,
				TeamId = player.TeamId,
				Age = player.Age,
				Position = player.Position,
				Height = player.Height,
				Weight = player.Weight,
				Country = player.Country,
				PhotoUrl = photoUrl
			};
		}

		public async Task<PlayersResponseDto> GetPlayerByIdAsync(int playerId)
		{
			var player = await _context.Players.FirstOrDefaultAsync(p => p.Id == playerId);

			if (player == null)
			{
				var response = new ResponseDto
				{
					IsSuccess = false,
					Message = "Invalid Id"
				};
			}

			return new PlayersResponseDto
			{
				Id = player.Id,
				Name = player.Name,
				TeamId = player.TeamId,
				Age = player.Age,
				Position = player.Position,
				Height = player.Height,
				Weight = player.Weight,
				Country = player.Country,
				PhotoUrl = player.PhotoUrl
			};
		}

		public async Task<PlayersResponseDto> UpdatePlayerAsync(int playerId, PlayerDto playerDto)
		{
			var player = await _context.Players.FindAsync(playerId);
			if (player == null) throw new ArgumentException("Player not found.");

			var team = await _context.Teams.FirstOrDefaultAsync(t => t.Id == playerDto.TeamId);
			if (team == null) throw new ArgumentException("Invalid TeamId. Team does not exist.");

			string updatedPhotoUrl = player.PhotoUrl;

			updatedPhotoUrl = playerDto.PhotoUrl;


			player.Name = playerDto.Name;
			player.TeamId = playerDto.TeamId;
			player.Age = playerDto.Age;
			player.Position = playerDto.Position;
			player.Height = playerDto.Height;
			player.Weight = playerDto.Weight;
			player.Country = playerDto.Country;
			player.PhotoUrl = updatedPhotoUrl;

			await _context.SaveChangesAsync();

			return new PlayersResponseDto
			{
				TeamId = player.TeamId,
				Age = player.Age,
				Id = player.Id,
				Name = player.Name,
				Position = player.Position,
				Height = player.Height,
				Weight = player.Weight,
				Country = player.Country,
				PhotoUrl = updatedPhotoUrl
			};
		}

		public async Task<bool> DeletePlayerAsync(int playerId)
		{
			var player = await _context.Players.FindAsync(playerId);
			if (player == null) return false;

			_context.Players.Remove(player);
			await _context.SaveChangesAsync();
			return true;
		}

	}
}


