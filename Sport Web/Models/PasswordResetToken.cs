using System.ComponentModel.DataAnnotations;

namespace Sport_Web.Models
{
	public class PasswordResetToken
	{
		[Key]
		public int TokenId { get; set; }        
		public int UserId { get; set; }         
		public string Token { get; set; }    
		public DateTime CreatedAt { get; set; } 
		public DateTime ExpiryDate { get; set; }
		public bool IsUsed { get; set; }

		public User User { get; set; }	
	}
}
