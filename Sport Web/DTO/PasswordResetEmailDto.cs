namespace Sport_Web.DTO
{
	public class PasswordResetEmailDto
	{
		public string ToEmail { get; set; }
		public string ResetToken { get; set; }
	}
}
