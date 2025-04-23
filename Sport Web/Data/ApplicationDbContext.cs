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
		//public DbSet<Sport_Web.Models.SectionContent> SectionContents { get; set; }
		//public DbSet<Home> Homes { get; set; }
		public DbSet<Team> Teams { get; set; }
		public DbSet<News> News { get; set; }	
		public DbSet<Match> Matches { get; set; }		
		//public DbSet<Articles> Articles { get; set; }
		public DbSet<Player> Players { get; set; }
		public DbSet<Cart> Carts { get; set; }
		public DbSet<CartItem> CartItems { get; set; }
		public DbSet<Delivery> Deliveries { get; set; }

		public DbSet<Product> Products { get; set; }

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
   .HasMany(c => c.News)
   .WithOne(t => t.Category)
   .HasForeignKey(t => t.CategoryId)
   .OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Team>()
		.HasMany(t => t.Players)
		.WithOne(p => p.Team)
		.HasForeignKey(p => p.TeamId)
		.OnDelete(DeleteBehavior.Cascade);
			modelBuilder.Entity<News>()
		   .HasOne(n => n.Teams)
		   .WithMany(t => t.News)
		   .HasForeignKey(n => n.TeamId)
		   .OnDelete(DeleteBehavior.SetNull);  // If a team is deleted, set the TeamId to null in the news


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

			//modelBuilder.Entity<Sport_Web.Models.SectionContent>()
			//	.HasOne(tc => tc.CategorySection)
			//	.WithMany(ct => ct.SectionContents)
			//	.HasForeignKey(tc => tc.CategorySectionId)
			//	.OnDelete(DeleteBehavior.Restrict);


			//	modelBuilder.Entity<Home>()
			//.HasOne(t => t.SectionContent)  
			//.WithMany(sc => sc.HomeContent)  
			//.HasForeignKey(t => t.SectionContentId)  
			//.OnDelete(DeleteBehavior.Cascade);

//			modelBuilder.Entity<Team>()
//	.HasOne(t => t.SectionContent)
//	.WithMany(sc => sc.Teams)
//	.HasForeignKey(t => t.CategoryId)
//	.OnDelete(DeleteBehavior.Cascade);
//			modelBuilder.Entity<News>()
//.HasOne(t => t.SectionContent)
//.WithMany(sc => sc.News)
//.HasForeignKey(t => t.CategoryId)
//.OnDelete(DeleteBehavior.Cascade);

			//			modelBuilder.Entity<Articles>()
			//.HasOne(t => t.SectionContent)
			//.WithMany(sc => sc.Articles)
			//.HasForeignKey(t => t.CategoryId)
			//.OnDelete(DeleteBehavior.Cascade);
			//modelBuilder.Entity<Articles>()
			//	.HasOne(a => a.Category)
			//	.WithMany()
			//	.HasForeignKey(a => a.CategoryId);

			//modelBuilder.Entity<Articles>()
			//	.HasOne(a => a.SectionContent)
			//	.WithMany(sc => sc.Articles)
			//	.HasForeignKey(a => a.CategoryId)
			//	.OnDelete(DeleteBehavior.Cascade);



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

			modelBuilder.Entity<User>()
		   .HasMany(u => u.Carts)
		   .WithOne(c => c.User)
		   .HasForeignKey(c => c.UserId);
			


			modelBuilder.Entity<Cart>()
	       .HasMany(c => c.Items)
	       .WithOne(ci => ci.Cart)
	       .HasForeignKey(ci => ci.CartId);


			// ✅ **CartItem & Cart (Many-to-One)**
			modelBuilder.Entity<CartItem>()
	 .HasOne(ci => ci.Product)
	 .WithMany()
	 .HasForeignKey(ci => ci.ProductId);

			// Cart -> Delivery (One-to-One)
			modelBuilder.Entity<Cart>()
				.HasOne(c => c.Delivery)
				.WithOne(d => d.Cart)
				.HasForeignKey<Delivery>(d => d.CartId);

			// Product -> Team (Many-to-One)
			modelBuilder.Entity<Product>()
				.HasOne(p => p.Team)
				.WithMany(t => t.Products)
				.HasForeignKey(p => p.TeamId);


			//modelBuilder.Entity<Articles>()
			//	  .HasOne(n => n.Category)
			//	  .WithMany()
			//	  .HasForeignKey(n => n.CategoryId)
			//	  .OnDelete(DeleteBehavior.Cascade);
		}
	}
}

