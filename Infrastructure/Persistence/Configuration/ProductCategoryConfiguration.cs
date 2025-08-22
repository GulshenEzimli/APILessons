using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration
{
	public class ProductCategoryConfiguration : IEntityTypeConfiguration<CategoryProduct>
	{
		public void Configure(EntityTypeBuilder<CategoryProduct> builder)
		{
			builder.HasKey(x => new { x.ProductId, x.CategoryId });

			builder.HasOne(cp => cp.Product).WithMany(p => p.ProductCategories)
					.HasForeignKey(cp => cp.ProductId).OnDelete(deleteBehavior: DeleteBehavior.Cascade);

			builder.HasOne(cp => cp.Category).WithMany(c => c.CategoryProducts)
					.HasForeignKey(cp => cp.CategoryId).OnDelete(deleteBehavior: DeleteBehavior.Cascade);
		}
	}
}
