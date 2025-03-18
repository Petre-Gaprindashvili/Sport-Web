using Microsoft.EntityFrameworkCore;
using Sport_Web.Models;
namespace Sport_Web.Data
{
	public class ApplicationDbContext: DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

		public DbSet<User> Users { get; set; }	
		public DbSet<PasswordResetToken> PasswordResetTokens { get; set; }

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
		}

	}
}
