using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration
{
	public class RoleConfiguration : IEntityTypeConfiguration<Role>
	{
		public void Configure(EntityTypeBuilder<Role> builder)
		{
			builder.ToTable("Roles");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).HasColumnType("uniqueidentifier").ValueGeneratedOnAdd();
		}
	}
}
