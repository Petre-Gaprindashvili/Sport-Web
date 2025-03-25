using Microsoft.EntityFrameworkCore;
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
		public DbSet<CategoryTab> CategoryTabs { get; set; }
		public DbSet<Team> Teams { get; set; }
		public DbSet<Player> Players { get; set; }

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

			modelBuilder.Entity<Team>()
		.HasMany(t => t.Players)
		.WithOne(p => p.Team)
		.HasForeignKey(p => p.TeamId)
		.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<CategoryTab>()
	.HasKey(s => s.Id); 

			modelBuilder.Entity<CategoryTab>()
				.HasOne(s => s.Category)  
				.WithMany(c => c.CategoryTabs)  
				.HasForeignKey(s => s.CategoryId)  
				.OnDelete(DeleteBehavior.Cascade);
			modelBuilder.Entity<CategoryTab>()
	  .Property(ct => ct.CategoryType)
	  .HasConversion(
		  v => v.ToString(),  // Convert the enum to string when saving to the database
		  v => (CategoryType)Enum.Parse(typeof(CategoryType), v) // Convert string back to enum when reading from the database
	  );

		}



	}
}
