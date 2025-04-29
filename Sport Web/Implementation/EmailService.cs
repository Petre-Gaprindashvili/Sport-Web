using Sport_Web.Abstraction;
using Sport_Web.DTO;
using System.Net;
using System.Net.Mail;
namespace Sport_Web.Implementation
{
	public class EmailService:IEmailService
	{
		private readonly IConfiguration _configuration;


		public EmailService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public async Task SendPasswordResetEmailAsync(PasswordResetEmailDto resetEmailDto)
		{
			string SmtpHost = _configuration["smtpsettings:host"];
			string SmtpUser = _configuration["smtpsettings:username"];
			string SmtpPassword = _configuration["smtpsettings:password"];

			var mail = new MailMessage();
			mail.From = new MailAddress(SmtpUser);
			mail.To.Add(resetEmailDto.ToEmail);
			mail.Subject = "Your Verification Token";
			var tokenUrl = $"http://localhost:58037/reset-password?token={resetEmailDto.ResetToken}";
			mail.Body = $"To reset your password, click the link below: {tokenUrl}";



			using (var client = new SmtpClient(SmtpHost))
			{
				client.Port = 587;
				client.Credentials = new NetworkCredential(SmtpUser, SmtpPassword);
				client.EnableSsl = true;

				client.Send(mail);
			};




		}

	}
}
