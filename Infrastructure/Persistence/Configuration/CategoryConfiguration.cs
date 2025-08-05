using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration
{
	public class CategoryConfiguration : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{
			Category category1 = new Category()
			{
				Id = 1,
				ParentId = 0,
				Name = "Elektrik",
				Priority = 1,
				Created = DateTime.Now,
				IsDeleted = false
			};

			Category category2 = new Category()
			{
				Id = 2,
				ParentId = 0,
				Name = "Moda",
				Priority = 2,
				Created = DateTime.Now,
				IsDeleted = false
			};

			Category category3 = new Category()
			{
				Id = 3,
				ParentId = 1,
				Name = "Bilgisayar",
				Priority = 1,
				Created = DateTime.Now,
				IsDeleted = false
			};

			Category category4 = new Category()
			{
				Id = 4,
				ParentId = 2,
				Name = "Kadın",
				Priority = 1,
				Created = DateTime.Now,
				IsDeleted = false
			};

			builder.HasData(category1, category2, category3, category4);
		}
	}
}
