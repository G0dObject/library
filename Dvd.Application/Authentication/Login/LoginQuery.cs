﻿using CQRS.Query;
using System.ComponentModel.DataAnnotations;

namespace Library.Application.Authentication.Login
{
	public class LoginQuery : IQuery<int>
	{
		public LoginQuery(string? username, string? password)
		{
			UserName = username;
			Password = password;
		}
		public LoginQuery()
		{

		}
		[Required]
		public string? UserName { get; set; }
		[Required]
		public string? Password { get; set; }
	}
}
