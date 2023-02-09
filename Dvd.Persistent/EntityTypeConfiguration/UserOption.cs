using Library.Domain.Entity.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Persistent.EntityTypeConfiguration
{
	internal class UserOption : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			_ = builder.HasKey(u => u.Id);
			_ = builder.Property(u => u.UserName).IsRequired();
			_ = builder.Property(u => u.Password).IsRequired();

		}
	}
}
