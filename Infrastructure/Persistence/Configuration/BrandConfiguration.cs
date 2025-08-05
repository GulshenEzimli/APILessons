using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration
{
	public class BrandConfiguration : IEntityTypeConfiguration<Brand>
	{
		public void Configure(EntityTypeBuilder<Brand> builder)
		{
			Brand brand = new Brand()
			{
				Id = 1,
				Name = "HP",
				Created = DateTime.Now,
				IsDeleted = false,
			};
			Brand brand2 = new Brand()
			{
				Id = 2,
				Name = "Dilvin",
				Created = DateTime.Now,
				IsDeleted = false,
			};
			Brand brand3 = new Brand()
			{
				Id = 3,
				Name = "DELL",
				Created = DateTime.Now,
				IsDeleted = false,
			};

			builder.HasData(brand,brand2,brand3);
		}
	}
}
