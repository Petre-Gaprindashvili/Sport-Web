using Microsoft.AspNetCore.Mvc;
using Sport_Web.Implementation;
using Sport_Web.Abstraction;
using Sport_Web.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace Sport_Web.Controllers
{
	
	[ApiController]
	[Route("api/users")]
	public class UserController : ControllerBase
	{
		private readonly IUserService _authenticationService;
		public UserController(IUserService authenticationService)
		{
			_authenticationService = authenticationService;	
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register( RegisterDto registerDto)
		{
			var user = await _authenticationService.RegisterAsync(registerDto);	
			if (user == null) return NotFound();
			return Ok(user);	
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
		{
			var user = await _authenticationService.LoginAsync(loginDto);
			if (user == null) 
			{

				return Unauthorized(new { Message = "Invalid email or password" });

			}

			return Ok(user);	
		}

		[HttpPost("password-reset/request")]
		public async Task<IActionResult> RequestPasswordReset( PasswordResetRequest passwordResetRequest)
		{
			var response = await _authenticationService.RequestPasswordResetAsync(passwordResetRequest);
			if (response == null)
			{
				return BadRequest("Incorrect Email");

			}
			return Ok(response);	
		}

		[HttpPost("reset-password")]
		public async Task<IActionResult> ResetPasswordAsync( [FromQuery,] string token, ResetPasswordDto resetPasswordDto)
		{

			var response =  await _authenticationService.ResetPasswordAsync(token, resetPasswordDto);
			if (response == null)
			{
				return BadRequest("Invalid Request");
			}
			return Ok(response);	

		}
		


	}
}
