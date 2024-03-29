﻿using Library.Application.Interfaces.BusinessLogic;
using Library.Domain.Entity.Tables;
using Library.Persistent.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistent.Repositories
{
	public class AuthorizationRepository : GenericRepository<User>, IAuthorizationRepository
	{
		private new readonly Context? _context;
		private readonly string _defaultRole = "User";
		public AuthorizationRepository(Context context) : base(context)
		{
			_context = context;
		}

		public async Task CreateAdmin()
		{
			if (Exist("Admin1").Result == false)
			{
				_ = await _context!.Users!.AddAsync(new User
				{
					Role = new Role
					{ Name = "Admin" }
				,
					Password = "Admin1",
					UserName = "Admin1"
				});
				_ = await _context!.SaveChangesAsync();
			}
		}

		public async Task CreateManager()
		{
			if (ExistRole("Manager").Result == false)
			{
				_ = await _context!.Roles!.AddAsync(new Role() { Name = "Manager" });
				_ = await _context!.SaveChangesAsync();
			}
		}

		public async Task<Role> GetDefaultRole()
		{
			if (await _context!.Roles!.FirstOrDefaultAsync(f => f.Name == _defaultRole) == null)
			{
				_ = await _context!.Roles!.AddAsync(new Role() { Name = _defaultRole });
				_ = await _context.SaveChangesAsync();
			}
			Role result = await _context!.Roles!.FirstAsync(f => f.Name == _defaultRole);
			return result;
		}
		public async Task<bool> Exist(string name)
		{
			User? c = await _context!.Users!.FirstOrDefaultAsync(u => u.UserName == name);
			return c != null;
		}
		public async Task<bool> ExistRole(string name)
		{
			Role? c = await _context!.Roles!.FirstOrDefaultAsync(u => u.Name == name);
			return c != null;
		}
		public async Task<Role> GetRole(int id)
		{
			await _context!.Roles!.LoadAsync();
			User? user = await _context!.Users!.FirstOrDefaultAsync(u => u.Id == id);
			return user!.Role ?? throw new NullReferenceException();
		}
	}
}
