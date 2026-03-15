using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Title).IsRequired().HasMaxLength(256);
            builder.Property(e => e.Description).IsRequired().HasMaxLength(256);
            builder.Property(e => e.Price).HasColumnType("decimal").IsRequired();
            builder.Property(e => e.BrandId).HasColumnType("int").IsRequired();
            builder.Property(e => e.Discount).HasColumnType("decimal").IsRequired();
            builder.Property(e => e.ImagePath).HasColumnType("nvarchar(256)").IsRequired();
            builder.Property(e => e.IsDeleted).IsRequired().HasDefaultValue(false);
            builder.Property(e => e.CreatedDate).IsRequired();

            builder.HasOne(e => e.Brand).WithMany(c => c.Products).HasForeignKey(d => d.BrandId).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
