namespace Sport_Web.DTO
{
	public class PlayerDto
	{

		public required string Name { get; set; }
		public int? TeamId { get; set; }
		public int Age { get; set; }
		public string? Position { get; set; }
		public string? Height { get; set; }
		public string? Weight { get; set; }
		public string? Country { get; set; }
		public string PhotoUrl { get; set; }
	}

	public class PlayersResponseDto
	{

		public int  Id { get; set; }	
		public required string Name { get; set; }
		public int? TeamId { get; set; }
		public int Age { get; set; }
		public string? Position { get; set; }
		public string? Height { get; set; }
		public string? Weight { get; set; }
		public string? Country { get; set; }
		public string? PhotoUrl { get; set; }

	}
}
