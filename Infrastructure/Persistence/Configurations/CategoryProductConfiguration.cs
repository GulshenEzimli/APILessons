using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class CategoryProductConfiguration : IEntityTypeConfiguration<CategoryProduct>
    {
        public void Configure(EntityTypeBuilder<CategoryProduct> builder)
        {
            builder.ToTable("CategoryProduct");
            builder.HasKey(e => new { e.ProductId, e.CategoryId });
            
            builder.HasOne(e => e.Category).WithMany(c => c.CategoryProducts).HasForeignKey(d => d.CategoryId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(e => e.Product).WithMany(c => c.CategoryProducts).HasForeignKey(d => d.ProductId).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
