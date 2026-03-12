using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class DetailConfiguration : IEntityTypeConfiguration<Detail>
    {
        public void Configure(EntityTypeBuilder<Detail> builder)
        {
            builder.ToTable("Details");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Title).IsRequired().HasMaxLength(256);
            builder.Property(e => e.Description).IsRequired().HasMaxLength(256);
            builder.Property(e => e.IsDeleted).IsRequired().HasDefaultValue(false);
            builder.Property(e => e.CreatedDate).IsRequired().HasDefaultValue(DateTime.Now);

            builder.HasOne(e => e.Category).WithMany(c => c.Details).HasForeignKey(d => d.CategoryId).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
