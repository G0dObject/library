using Library.Domain.Entity.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Persistent.EntityTypeConfiguration
{
	internal class BookOption : IEntityTypeConfiguration<Book>
	{
		public void Configure(EntityTypeBuilder<Book> builder)
		{
			_ = builder.HasKey(r => r.Id);
			_ = builder.Property(r => r.Name);
			_ = builder.Property(r => r.Amount).HasDefaultValue(0);
			_ = builder.Property(r => r.Description);
		}
	}
}
