using System.ComponentModel.DataAnnotations;

namespace Sport_Web.DTO
{
	public class UserDto
	{
		public int UserId { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }
		public string? Role { get; set; }
        public bool isActive { get; set; }	
       
	}

	public class RegisterDto
	{
		[Required]
		[StringLength(50, MinimumLength = 3)]
		public string UserName { get; set; }
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		[Required]
		[StringLength(100, MinimumLength = 6)]
		public string Password { get; set; }
	}

	public class LoginDto
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		public string Password { get; set; }
	}

	public class PasswordResetEmailDto
	{
		public string ToEmail { get; set; }
		public string ResetToken { get; set; }
	}
}
