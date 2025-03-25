using Microsoft.AspNetCore.Mvc;
using Sport_Web.Implementation;
using Sport_Web.Abstraction;
using Sport_Web.DTO;
using Microsoft.AspNetCore.Authorization;

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
		public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
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
		public async Task<IActionResult> RequestPasswordReset([FromBody] string email)
		{
			var response = await _authenticationService.RequestPasswordResetAsync(email);
			if (response == null)
			{
				return BadRequest("Incorrect Email");

			}
			return Ok(response);	
		}

		[HttpPost("reset-password")]
		public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordDto resetPasswordDto)
		{

			var response =  await _authenticationService.ResetPasswordAsync(resetPasswordDto);
			if (response == null)
			{
				return BadRequest("Invalid Request");
			}
			return Ok(response);	

		}


	}
}
