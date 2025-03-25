using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sport_Web.Abstraction;

namespace Sport_Web.Controllers
{
	[ApiController]
	[Route("api/admin/users")]
	[Authorize(Roles = "Admin")]
	public class UserManagementController : Controller
	{
		private readonly IUserService _userService;

		public UserManagementController(IUserService userService)
		{
			_userService = userService;		
		}

		[HttpGet("GetAll")]
		public async Task<IActionResult> GetAllUsers()
		{
			var getAllUsers = await _userService.GetAllUsersAsync();	
			if (getAllUsers == null) return NotFound();
			return Ok(getAllUsers);	
		}

		[HttpGet("GeUser/{id}")]
		public async Task<IActionResult> GetUserById(int id)
		{
			var getUser = await _userService.GetUserByIdAsync(id);
			if (getUser == null) return NotFound("User not found");	
			return Ok(getUser);	
		}


		[HttpPost("request-password-reset")]
		public async Task<IActionResult> RequestPasswordReset(string email)
		{
			var result = await _userService.RequestPasswordResetAsync(email);	
			if (result == null) return NotFound("User not found");	
			return Ok(result);	
		}

		[HttpPut("update-role/{id}")]	
		public async Task<IActionResult> UpdateUserRole(int id,  string newRole)
		{
			var updateRole = await _userService.UpdateUserRoleAsync(id, newRole);	
			if (updateRole == null) return NotFound();
			return Ok(updateRole);	
		}

		[HttpPut("deactivate/{id}")]
		public async Task<IActionResult> DeactivateUser(int id)
		{
			var result = await _userService.DeactivateUserAsync(id);
			if (!result) return BadRequest("User not found or already inactive.");
			return Ok(result);	
		}

		[HttpPut("reactivate/{id}")]
		public async Task<IActionResult> ReactivateUser(int id)
		{
			var result = await _userService.ReactivateUserAsync(id);
			if (!result) return BadRequest("User not found or already active.");

			return Ok(result);
		}

		[HttpDelete("DeleteUser/{id}")]
		public async Task<IActionResult> DeleteUserByid(int id)
		{
			var deletedUser =  await _userService.DeleteUserByIdAsync(id);
			if (deletedUser == null) return NotFound("User not found");
			return Ok(deletedUser);
	

		}
	}
}

