using System.ComponentModel.DataAnnotations;

namespace Sport_Web.DTO
{
	public class MatchesDto
	{
		public int HomeTeamId { get; set; }
		public IFormFile HomeTeamLogo { get; set; }
		public int AwayTeamId { get; set; }
		public IFormFile AwayTeamLogo { get; set; }

		public int HomeScore { get; set; }
		public int AwayScore { get; set; }
		[DataType(DataType.DateTime)]
		public DateTime MatchDate { get; set; }
	}

	public class MatchesResponseDto
	{
		public int Id { get; set; }
		public string HomeTeamName { get; set; }
	    public string HomeTeamLogo { get; set; }  
		public string AwayTeamName { get; set; }
	    public string AwayTeamLogo { get; set; }
		public int HomeScore { get; set; }
		public int AwayScore { get; set; }
		public DateTime MatchDate { get; set; }
		public string Winner { get; set; }
		public int CategoryId { get; set; } 
		public string CategoryName { get; set; }
	}

}
