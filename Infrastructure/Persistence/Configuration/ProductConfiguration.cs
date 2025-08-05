using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration
{
	public class ProductConfiguration : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			Product p1 = new Product
			{
				Id = 1,
				Title = "Dell XPS 15",
				Description = "Yüksek performanslı Intel Core i7 işlemci, 16 GB RAM ve 512 GB SSD ile yazılım geliştirme ve profesyonel kullanım için ideal bir ultrabook.",
				Price = 2000,
				Discount = 2,
				BrandId = 3,
				Created = DateTime.Now,
				IsDeleted = false,
			};

			Product p2 = new Product
			{
				Id = 2,
				Title = "HP  1467 VP2",
				Description = "HP VP2 işlemcisi ile hızlı ve verimli çalışma sunar. Retina ekranı ve uzun pil ömrüyle geliştiriciler için tasarlandı.",
				Price = 1500,
				Discount = 1.5m,
				BrandId = 1,
				Created = DateTime.Now,
				IsDeleted = false,
			};

			Product p3 = new Product
			{
				Id = 3,
				Title = "HP  1467 VP2",
				Description = "Aktif gürültü engelleme özelliği ve yüksek ses kalitesiyle müzik keyfini zirveye taşıyan kablosuz kulaklık.",
				Price = 400,
				Discount = 3,
				BrandId = 1,
				Created = DateTime.Now,
				IsDeleted = false,
			};

			builder.HasData(p1, p2, p3);
		}
	}
}
