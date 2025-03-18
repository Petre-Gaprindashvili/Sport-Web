﻿using System.ComponentModel.DataAnnotations;

namespace Sport_Web.DTO
{
	public class RegisterDto
	{
		[Required]
		[StringLength(50, MinimumLength = 3)]
		public string UserName { get; set; }
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		[Required]
		[StringLength(100, MinimumLength = 6)]
		public string Password { get; set; }
	}
}
