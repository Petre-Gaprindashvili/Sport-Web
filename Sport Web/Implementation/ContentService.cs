using Microsoft.AspNetCore.Components.Sections;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client.TelemetryCore.TelemetryClient;
using Sport_Web.Abstraction;
using Sport_Web.Data;
using Sport_Web.DTO;
using Sport_Web.Models;
using System.Net.Sockets;
using static System.Collections.Specialized.BitVector32;

namespace Sport_Web.Implementation
{
	public class ContentService : IcontentService
	{
		private readonly ApplicationDbContext _context;
		private readonly IImageUploadService _imageUploadService;

		public ContentService(ApplicationDbContext context, IImageUploadService imageUploadService)
		{
			_context = context;
			_imageUploadService = imageUploadService;
		}

		public async Task<TeamResponseDto> AddTeamAsync(TeamDto teamDto)
		{

			string imageUrl = await _imageUploadService.UploadImageAsync(teamDto.LogoUrl);

			var team = new Team
			{

				Name = teamDto.TeamName,
				CategoryId = teamDto.CategorySectionId,
				LogoUrl = imageUrl,
			};
			_context.Teams.Add(team);
			await _context.SaveChangesAsync();

			return new TeamResponseDto
			{

				Id = team.Id,
				TeamName = team.Name,
				LogoUrl = imageUrl,
				CategorySectionId = team.CategoryId,


			};
		}

		public async Task<HomeResponseDto> AddHomeAsync(HomeDto homeDto)
		{
			// Step 1: Check if the CategorySectionId exists in the SectionContents table
			var sectionContent = await _context.SectionContents
				.FirstOrDefaultAsync(k => k.CategorySectionId == homeDto.CategorySectionId);

			// Step 2: If the sectionContent doesn't exist, throw an exception or handle the error
			if (sectionContent == null)
			{
				// Handle the case where the CategorySectionId is invalid
				throw new Exception("The provided CategorySectionId does not exist in the SectionContents table.");
			}

			// Step 3: Upload the image
			string imageUrl = await _imageUploadService.UploadImageAsync(homeDto.LogoUrl);

			// Step 4: Create a new Home object with the valid SectionContentId
			var home = new Home
			{
				Title = homeDto.Tittlee,
				SectionContentId = sectionContent.Id, 
				// Ensure this is the valid Id from SectionContents
				ImageUrl = imageUrl,
				Description = homeDto.Description,
			};

			// Step 5: Add the Home record to the database
			_context.Homes.Add(home);
			await _context.SaveChangesAsync();

			// Step 6: Return the HomeResponseDto with the appropriate values
			return new HomeResponseDto
			{
				Id = home.Id,  // The Home ID, which is the unique identifier for the newly created Home
       	   	 			   //Tabname = home.Title,
				SectionName = home.SectionContent.ContentType,  // Assuming SectionName refers to Home Title
				LogoUrl = imageUrl,
				CategorySectionId = sectionContent.CategorySectionId,  // Return the SectionContentId that was matched
			};
		}
	




		//public async Task<HomeResponseDto> AddHomeAsync(HomeDto homeDto)
		//{
		//	var sectionContent = await _context.SectionContents
		//	.FirstOrDefaultAsync(k => k.CategorySectionId == homeDto.CategorySectionId);
		//	string imageUrl = await _imageUploadService.UploadImageAsync(homeDto.LogoUrl);

		//	var home = new Home
		//	{

		//		Title = homeDto.Tittle,
		//		SectionContentId = sectionContent.Id,
		//		ImageUrl = imageUrl,
		//		Description = homeDto.Description,

		//	};
		//	_context.Homes.Add(home);
		//	await _context.SaveChangesAsync();

		//	return new HomeResponseDto
		//	{

		//		Id = home.Id,
		//		ArticeName = home.Title,
		//		SectionName = home.Description,
		//		LogoUrl = imageUrl,
		//		CategorySectionId = home.SectionContent.CategorySectionId,


	//};

	//	}





		public async Task<List<TeamResponseDto>> GetAllTeamsAsync()
		{
			var getAllTeams = await _context.Teams.Select(t => new TeamResponseDto
			{
				//Id = t.Id,
				TeamName = t.Name,
				LogoUrl = t.LogoUrl,
				CategorySectionId = t.CategoryId,
				//CategorySectionId = t.SectionContent.Select(i => i.CategorySectionId).FirstOrDefault(),

			}).ToListAsync();
			if (getAllTeams == null) return null;
			return getAllTeams;
		}


		public async Task<List<TabContentDto>> GetTabContentAsync(int categorySectionId)
		{

			var teams = await _context.Teams
					.Where(t => t.CategoryId == categorySectionId)
					.Select(t => new  TabContentDto
					{

						SectionId = t.Id,					
						Content = new  TeamResponseDto
						{
							Id = t.Id,
							SectionName = t.Name,	
							CategorySectionId = categorySectionId,	
							LogoUrl = t.LogoUrl,						

						}
					})
					.ToListAsync();


			var home = await _context.Homes
				.Where(s => s.SectionContentId == categorySectionId)
				.Select(t => new TabContentDto
				{

					SectionId = t.Id,
					Content = new HomeResponseDto
					{
						Id = t.Id,
						SectionNamea = t.Title,
						CategorySectionId = categorySectionId,
						LogoUrl = t.ImageUrl,

					}
				})
				.ToListAsync();


			var cominedcontent = new List<TabContentDto>();	
			cominedcontent.AddRange(teams);
			cominedcontent.AddRange (home);	
			return cominedcontent;

		}
	}

}







	










	

