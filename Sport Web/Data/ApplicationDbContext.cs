using Microsoft.AspNetCore.Components.Sections;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Sport_Web.Enums;
using Sport_Web.Models;
namespace Sport_Web.Data
{
	public class ApplicationDbContext: DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

		public DbSet<User> Users { get; set; }	
		public DbSet<PasswordResetToken> PasswordResetTokens { get; set; }
		public DbSet<Category> Categories { get; set; }	
		public DbSet<CategorySection> categorySections { get; set; }
		public DbSet<Sport_Web.Models.SectionContent> SectionContents { get; set; }

		public DbSet<Home> Homes { get; set; }

		public DbSet<Team> Teams { get; set; }
		public DbSet<Player> Players { get; set; }
		public DbSet<Match> Matches { get; set; }		

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<PasswordResetToken>()
	        .HasKey(prt => prt.TokenId);
			modelBuilder.Entity<PasswordResetToken>()
				 .HasOne(prt => prt.User)           
			.WithMany(u => u.PasswordResetTokens) 
			.HasForeignKey(prt => prt.UserId)     
			.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Category>()
	   .HasMany(c => c.SubCategories)
	   .WithOne(c => c.ParentCategory)
	   .HasForeignKey(c => c.ParentCategoryId)
	   .OnDelete(DeleteBehavior.SetNull);

			modelBuilder.Entity<Category>()
	   .HasMany(c => c.Teams)
	   .WithOne(t => t.Category)
	   .HasForeignKey(t => t.CategoryId)
	   .OnDelete(DeleteBehavior.Cascade);
			modelBuilder.Entity<Category>()
   .HasMany(c => c.homes)
   .WithOne(t => t.Category)
   .HasForeignKey(t => t.SectionContentId)
   .OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Team>()
		.HasMany(t => t.Players)
		.WithOne(p => p.Team)
		.HasForeignKey(p => p.TeamId)
		.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Category>()
	.HasKey(s => s.Id); 

			modelBuilder.Entity<CategorySection>()
				.HasOne(s => s.Category)  
				.WithMany(c => c.CategorySections)  
				.HasForeignKey(s => s.CategoryId)  
				.OnDelete(DeleteBehavior.Cascade);
			modelBuilder.Entity<CategorySection>()
	  .Property(ct => ct.CategoryType)
	  .HasConversion(
		  v => v.ToString(),
		  v => (CategoryType)Enum.Parse(typeof(CategoryType), v) 
	  );

		//	modelBuilder.Entity<Sport_Web.Models.SectionContent>()
		//.HasKey(tc => tc.Id);  // Primary key

			modelBuilder.Entity<Sport_Web.Models.SectionContent>()
				.HasOne(tc => tc.CategorySection)
				.WithMany(ct => ct.SectionContents)
				.HasForeignKey(tc => tc.CategorySectionId)
				.OnDelete(DeleteBehavior.Restrict);

		
			modelBuilder.Entity<Home>()
		.HasOne(t => t.SectionContent)  
		.WithMany(sc => sc.HomeContent)  
		.HasForeignKey(t => t.SectionContentId)  
		.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Team>()
	.HasOne(t => t.SectionContent)  
	.WithMany(sc => sc.Teams) 
	.HasForeignKey(t => t.CategoryId) 
	.OnDelete(DeleteBehavior.Cascade);


			modelBuilder.Entity<Match>()
		   .HasOne(m => m.HomeTeam)
		   .WithMany(t => t.HomeMatches)
		   .HasForeignKey(m => m.HomeTeamId)
		   .OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Match>()
				.HasOne(m => m.AwayTeam)
				.WithMany(t => t.AwayMatches)
				.HasForeignKey(m => m.AwayTeamId)
				.OnDelete(DeleteBehavior.Restrict);



		}
	}
}
