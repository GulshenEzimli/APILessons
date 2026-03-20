using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Priority).HasColumnType("int").IsRequired();
            builder.Property(c => c.ParentId).HasColumnType("int").IsRequired(false);
            builder.Property(c => c.IsDeleted).IsRequired().HasDefaultValue(false);
            builder.Property(c => c.CreatedDate).IsRequired();

            builder.HasOne(c => c.Parent).WithMany(c => c.ChildCategories).HasForeignKey(c => c.ParentId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(c => c.Details).WithOne(d => d.Category).HasForeignKey(d => d.CategoryId).OnDelete(DeleteBehavior.Restrict); 

        }
    }
}
