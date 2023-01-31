using Library.Application.Interfaces;
using Library.Domain.Entity.Tables;
using Library.Persistent.EntityTypeConfiguration;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace Library.Persistent
{
	public class Context : DbContext, IContext
	{
		public Context(DbContextOptions<Context> dbContextOptions) : base(dbContextOptions) { _ = Initializer.Initialize(this); }

		public DbSet<User>? Users { get; set; }
		public DbSet<Role>? Roles { get; set; }
		public DbSet<Disk>? Disks { get; set; }
		public DbSet<Rented>? Renteds { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				var contextOptionsBuilder = optionsBuilder.UseSqlite(ConfigurationManager.ConnectionStrings["SqLite"].ConnectionString);
			}
		}

		protected override void OnModelCreating(ModelBuilder option)
		{
			_ = option.ApplyConfiguration(new UserOption());
			_ = option.ApplyConfiguration(new RoleOption());
			_ = option.ApplyConfiguration(new DiskOption());
			_ = option.ApplyConfiguration(new RentedOption());
			base.OnModelCreating(option);
		}
	}
}
