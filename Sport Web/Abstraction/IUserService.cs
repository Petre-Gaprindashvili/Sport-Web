using Microsoft.AspNetCore.Mvc;
using Sport_Web.DTO;

namespace Sport_Web.Abstraction
{
	public interface IUserService
	{
		Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);
		Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
		Task<ResponseDto> RequestPasswordResetAsync( PasswordResetRequest passwordResetRequest);
		Task<ResponseDto> ResetPasswordAsync(string token, ResetPasswordDto resetPasswordDto);

		// Admin-specific methods:
		Task<List<UserDto>> GetAllUsersAsync();
		Task<UserDto> GetUserByIdAsync(int id);		
		Task<bool> UpdateUserRoleAsync(int userId, string newRole);
		Task<bool> DeactivateUserAsync(int id);
		Task<bool> ReactivateUserAsync(int id);
		Task<bool> DeleteUserByIdAsync(int id);	

	}
}

