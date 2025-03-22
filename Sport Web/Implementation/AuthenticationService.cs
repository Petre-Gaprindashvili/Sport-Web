using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sport_Web.Abstraction;
using Sport_Web.Data;
using Sport_Web.DTO;
using Sport_Web.Models;
using System.Security.Claims;
using System.Text.RegularExpressions;


namespace Sport_Web.Implementation
{
	public class AuthenticationService : Sport_Web.Abstraction.IAuthenticationService
	{
		private readonly ApplicationDbContext _context;
		private readonly IEmailService _emailService;	
		private readonly IPasswordHasher<User> _passwordHasher;
		private readonly IHttpContextAccessor _httpContextAccessor;



		public AuthenticationService(ApplicationDbContext context,  IPasswordHasher<User> passwordHasher, IHttpContextAccessor httpContextAccessor, IEmailService emailService)
		{
			_context = context;
			_passwordHasher = passwordHasher;
			_httpContextAccessor = httpContextAccessor;
			_emailService = emailService;
		}

		public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
		{
			var emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
			if (!Regex.IsMatch(registerDto.Email, emailPattern))
			{
				throw new Exception("Invalid email format.");
			}
			var usernamePattern = @"^[a-zA-Z0-9]+$";
			if (!Regex.IsMatch(registerDto.UserName, usernamePattern))
			{
				throw new Exception("Invalid username format. Only alphanumeric characters are allowed.");

			}
			var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == registerDto.Email);
			if (existingUser != null)
			{
				throw new Exception("User already exists");

			}

			var user = new User
			{
				UserName = registerDto.UserName,
				Email = registerDto.Email,
				PasswordHash = _passwordHasher.HashPassword(null, registerDto.Password),
				//Role = "User",
				IsActive = true,
				CreatedAt = DateTime.UtcNow,
			};
			string adminPattern = @"^Admin";
			string localPart = user.Email.Split('@')[0];

			if (Regex.IsMatch(localPart, adminPattern))
			{
				user.Role = "Admin";	
			}
			else
			{
				user.Role = "User";
			}

			_context.Users.Add(user);
			_context.SaveChanges();
			return new UserDto
			{
				UserId = user.UserId,
				UserName = user.UserName,
				Email = user.Email,

			};
		}

		public async Task<IActionResult> LoginAsync(LoginDto loginDto)
		{
			var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
			if (user == null || _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginDto.Password) != PasswordVerificationResult.Success)
			{
				return new UnauthorizedObjectResult("Invalid credentials.");

			}

			var claims = new List<Claim>

	{
			new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
			new Claim(ClaimTypes.Name, user.UserName),
			new Claim(ClaimTypes.Email, user.Email),
			new Claim(ClaimTypes.Role, user.Role)
		};

			var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
			var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
			var context = _httpContextAccessor.HttpContext;
			if (context != null)
			{

				await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
			}
			return new OkObjectResult("Login successful");


		}

		public async Task<PasswordResetResponseDto> RequestPasswordResetAsync(string email)
		{
			var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
			if (user == null)
			{
				return new PasswordResetResponseDto
				{
					Message = "User not found",
					IsSuccess = false
				};

			
			}

			var existingToken = await _context.PasswordResetTokens
	   .FirstOrDefaultAsync(t => t.UserId == user.UserId && !t.IsUsed && t.ExpiryDate > DateTime.UtcNow);

			if (existingToken != null)
			{
				return new PasswordResetResponseDto
				{
					Message = "A password reset link is already sent. Please check your email.",
					IsSuccess = false
				};
			}

			var token = new PasswordResetToken
			{
				UserId = user.UserId,
				Token = Guid.NewGuid().ToString(),
				CreatedAt = DateTime.UtcNow,
				ExpiryDate = DateTime.UtcNow.AddHours(1),
				IsUsed = false,

			};

			_context.PasswordResetTokens.Add(token);
			_context.SaveChanges();

			var resetEmailDto = new PasswordResetEmailDto
			{
				ToEmail = email,
				ResetToken = token.Token,
			};

			await _emailService.SendPasswordResetEmailAsync(resetEmailDto);	
			

			return new PasswordResetResponseDto
			{
				Message = "Password reset link has been sent to your email.",
				IsSuccess = true
			};

		}

		public async Task<PasswordResetResponseDto> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
		{
			if(resetPasswordDto.NewPassword != resetPasswordDto.ConfirmPassword)
			{
				return new PasswordResetResponseDto
				{
					Message = "Password and confirmation password do not match.",
					IsSuccess = false
				};
			}

			var resetToken = await _context.PasswordResetTokens.FirstOrDefaultAsync(t=>t.Token == resetPasswordDto.Token && t.ExpiryDate> DateTime.UtcNow && !t.IsUsed);
			if (resetToken == null)
			{
				return new PasswordResetResponseDto
				{
					Message = "Invalid or expired token.",
					IsSuccess = false
				};
			}

			var user = await _context.Users.FirstOrDefaultAsync(u=>u.UserId == resetToken.UserId);	
			if (user == null)
			{
				return new PasswordResetResponseDto
				{
					Message = "User not found.",
					IsSuccess = false
				};
			}
			user.PasswordHash = _passwordHasher.HashPassword( null , resetPasswordDto.NewPassword);	
			resetToken.IsUsed = true;	
			_context.PasswordResetTokens.Update(resetToken);	
			await _context.SaveChangesAsync();
			return new PasswordResetResponseDto
			{
				Message = "Password has been successfully reset.",
				IsSuccess = true
			};

		}



	}
}
