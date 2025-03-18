using Sport_Web.DTO;

namespace Sport_Web.Abstraction
{
	public interface IEmailService
	{

		Task SendPasswordResetEmailAsync(PasswordResetEmailDto resetEmailDto);

	}
}
