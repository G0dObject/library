using Library.Domain.Entity.Tables;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Interfaces
{
	public interface IContext
	{
		public DbSet<User>? Users { get; set; }
		public DbSet<Role>? Roles { get; set; }
		public DbSet<Book>? Books { get; set; }
		public DbSet<Rented>? Renteds { get; set; }
	}
}
