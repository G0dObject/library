﻿using Library.Domain.Entity.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Persistent.EntityTypeConfiguration
{
    internal class RoleOption : IEntityTypeConfiguration<Role>
	{
		public void Configure(EntityTypeBuilder<Role> builder)
		{
			_ = builder.HasKey(r => r.Id);
			_ = builder.Property(r => r.Name);

		}
	}
}
