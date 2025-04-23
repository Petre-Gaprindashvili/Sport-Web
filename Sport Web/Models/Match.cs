﻿namespace Sport_Web.Models
{
	public class Match
	{

		public int Id { get; set; }
		public int HomeTeamId { get; set; }
		public int AwayTeamId { get; set; }
		public int? HomeScore { get; set; }
		public int? AwayScore { get; set; }
		public DateTime MatchDate { get; set; }
		public string? HomeTeamlogo { get; set; }
		public string? AwayTeamlogo { get; set; }

		public Team HomeTeam { get; set; }
		public Team AwayTeam { get; set; }
	    //public SectionContent SectionContent { get; set; }	

	}
}




