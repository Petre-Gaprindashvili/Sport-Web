using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sport_Web.Abstraction;
using Sport_Web.Data;
using Sport_Web.DTO;
using Sport_Web.Models;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text.RegularExpressions;


namespace Sport_Web.Implementation
{
    public class UserService : Sport_Web.Abstraction.IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;	
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IHttpContextAccessor _httpContextAccessor;



        public UserService(ApplicationDbContext context,  IPasswordHasher<User> passwordHasher, IHttpContextAccessor httpContextAccessor, IEmailService emailService)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _httpContextAccessor = httpContextAccessor;
            _emailService = emailService;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            var emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (!Regex.IsMatch(registerDto.Email, emailPattern))
            {
                return new AuthResponseDto
                {
                    Message = "Invalid email format.",
                    IsSuccess = false
                };
            }
            var usernamePattern = @"^[a-zA-Z0-9]+$";
            if (!Regex.IsMatch(registerDto.UserName, usernamePattern))
            {
                return new AuthResponseDto
                {
                    Message = "Username can only contain alphanumeric characters.",
                    IsSuccess = false
                };

            }
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == registerDto.Email);
            if (existingUser != null)
            {
                return new AuthResponseDto
                {
                    Message = "User already exists.",
                    IsSuccess = false
                };

            }

			// Create a new User object with the provided registration data.
			// The password is securely hashed, and the account is marked as active with the current timestamp.
			var user = new User
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                PasswordHash = _passwordHasher.HashPassword(null, registerDto.Password),
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
            };

			// Determine the user's role based on the email local part (before the '@').
			// If the local part starts with "Admin", assign the "Admin" role. Otherwise, assign the "User" role.
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
          var userDto =  new UserDto
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email,
                Role = user.Role,	
                isActive = user.IsActive,	

            };
            return new AuthResponseDto
            {
                Message = "User registered successfully.",
                IsSuccess = true,
                Role = user.Role,	
                
            };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
            if (user == null || _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginDto.Password) != PasswordVerificationResult.Success)
            {

                return new AuthResponseDto { IsSuccess = false, Message = "User not found." };

            }
            if (!user.IsActive)
            {
                return new AuthResponseDto { IsSuccess = false, Message = "Your account is deactivated. Contact admin." };

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


            return new AuthResponseDto { IsSuccess = true, Message = "Login Successful", Role = user.Role, UserId = user.UserId };


        }

        public async Task<ResponseDto> RequestPasswordResetAsync(PasswordResetRequest passwordResetRequest)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == passwordResetRequest.Email);
            if (user == null)
            {
                return new ResponseDto
                {
                    Message = "User not found",
                    IsSuccess = false
                };

            
            }

            var existingToken = await _context.PasswordResetTokens
       .FirstOrDefaultAsync(t => t.UserId == user.UserId && !t.IsUsed && t.ExpiryDate > DateTime.UtcNow);

            if (existingToken != null)
            {
                return new ResponseDto
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
                ExpiryDate = DateTime.UtcNow.AddMinutes(1),
                IsUsed = false,


            };

            _context.PasswordResetTokens.Add(token);
            _context.SaveChanges();

            var resetEmailDto = new PasswordResetEmailDto
            {
                ToEmail = passwordResetRequest.Email,
                ResetToken = token.Token,
            };

            await _emailService.SendPasswordResetEmailAsync(resetEmailDto);	
            

            return new ResponseDto
            {
                Message = "Password reset link has been sent to your email.",
                IsSuccess = true
            };

        }

        public async Task<ResponseDto> ResetPasswordAsync(string token, ResetPasswordDto resetPasswordDto)
        {
            if(resetPasswordDto.NewPassword != resetPasswordDto.ConfirmPassword)
            {
                return new ResponseDto
                {
                    Message = "Password and confirmation password do not match.",
                    IsSuccess = false
                };
            }

            var resetToken = await _context.PasswordResetTokens.FirstOrDefaultAsync(t=> t.ExpiryDate> DateTime.UtcNow && !t.IsUsed);
            if (resetToken == null)
            {
                return new ResponseDto
                {
                    Message = "Invalid or expired token.",
                    IsSuccess = false
                };
            }

            var user = await _context.Users.FirstOrDefaultAsync(u=>u.UserId == resetToken.UserId);	
            if (user == null)
            {
                return new ResponseDto
                {
                    Message = "User not found.",
                    IsSuccess = false
                };
            }
            user.PasswordHash = _passwordHasher.HashPassword( null , resetPasswordDto.NewPassword);	
            resetToken.IsUsed = true;	
            _context.PasswordResetTokens.Update(resetToken);	
            await _context.SaveChangesAsync();
            return new ResponseDto
            {
                Message = "Password has been successfully reset.",
                IsSuccess = true
            };

        }
        // Admin-related methods
        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            var getAllUsers = _context.Users.Select(u => new UserDto
            {
                UserId = u.UserId,
                UserName = u.UserName,
                Email = u.Email,
                Role = u.Role,
                isActive = u.IsActive,
            }).ToList();
            return getAllUsers;	

        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u=>u.UserId==id);
            if (user == null) return null;
            return new UserDto
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email,
                Role = user.Role,
                isActive = user.IsActive,
            };
        }
    

        public async Task<bool> UpdateUserRoleAsync(int userId, string newRole)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u=>u.UserId == userId);	
            if (user == null) return false;
            
            user.Role = newRole;
            await _context.SaveChangesAsync();	
            return true;

        }

        public async Task<bool> DeactivateUserAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
            if (user == null || !user.IsActive) return false;

            user.IsActive = false;
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<bool> ReactivateUserAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
            if (user == null || user.IsActive) return false;

            user.IsActive = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUserByIdAsync(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            if (user == null) return false;
            _context.Users.Remove(user);
            _context.SaveChanges();
            return true;

        }

    

    }
}
