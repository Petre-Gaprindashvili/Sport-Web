using Microsoft.AspNetCore.Mvc;
using Sport_Web.DTO;

namespace Sport_Web.Abstraction
{
	public interface IAuthenticationService
	{
		Task<UserDto> RegisterAsync(RegisterDto registerDto);
		Task<IActionResult> LoginAsync(LoginDto loginDto);
		Task<PasswordResetResponseDto> RequestPasswordResetAsync(string email);
		Task<PasswordResetResponseDto> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);


	}
}
