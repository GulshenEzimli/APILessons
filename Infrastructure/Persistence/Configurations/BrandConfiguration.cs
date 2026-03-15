using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.ToTable("Brands");
            builder.HasKey(b => b.Id);  

            builder.Property(b => b.Name).IsRequired().HasMaxLength(100);
            builder.Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
            builder.Property(b => b.CreatedDate).IsRequired();

        }
    }
}
