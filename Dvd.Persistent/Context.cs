using Library.Application.Interfaces;
using Library.Domain.Entity.Tables;
using Library.Persistent.EntityTypeConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MySql.EntityFrameworkCore;
using System.Configuration;

namespace Library.Persistent
{
	public class Context : DbContext, IContext
	{
		public Context(DbContextOptions<Context> dbContextOptions) : base(dbContextOptions) { Initializer.Initialize(this); }

		public DbSet<User>? Users { get; set; }
		public DbSet<Role>? Roles { get; set; }
		public DbSet<Book>? Books { get; set; }
		public DbSet<Rented>? Renteds { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				bool boolean = bool.Parse(ConfigurationManager.AppSettings["IsSqlLite"] ?? "true");
				if (boolean)
				{
					string g = (ConfigurationManager.ConnectionStrings["SqLite"].ConnectionString);
					DbContextOptionsBuilder dbb = optionsBuilder.UseSqlite(g);
				}
				else
				{
					string g = (ConfigurationManager.ConnectionStrings["MySql"].ConnectionString);
					DbContextOptionsBuilder dbb = optionsBuilder.UseMySQL(g);
				}
			}
		}

		protected override void OnModelCreating(ModelBuilder option)
		{
			_ = option.ApplyConfiguration(new UserOption());
			_ = option.ApplyConfiguration(new RoleOption());
			_ = option.ApplyConfiguration(new BookOption());
			_ = option.ApplyConfiguration(new RentedOption());
			base.OnModelCreating(option);
		}

	}
}
