using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration
{
	public class DetailConfiguration : IEntityTypeConfiguration<Detail>
	{
		public void Configure(EntityTypeBuilder<Detail> builder)
		{
			Detail detail1 = new Detail()
			{
				Id = 1,
				Title = "Pil Kapasitesi",
				Description = "Cihazın ne kadar süre çalışabileceğini belirler. mAh (miliamper-saat) cinsinden ölçülür. Daha yüksek değer, daha uzun kullanım süresi sağlar.",
				CategoryId = 1,
				Created = DateTime.Now,
				IsDeleted = false,
			};

			Detail detail2 = new Detail()
			{
				Id = 2,
				Title = "Ekran Özelliği",
				Description = "Cihazın ekran tipi ve çözünürlüğü. Örn: AMOLED, LCD, 1080x2400 piksel. Görüntü kalitesi ve kullanıcı deneyimini etkiler.",
				CategoryId = 1,
				Created = DateTime.Now,
				IsDeleted = false,
			};

			Detail detail3 = new Detail()
			{
				Id = 3,
				Title = "Enerji Tüketimi",
				Description = "Cihazın çalışma sırasında harcadığı elektrik miktarıdır. Watt (W) cinsinden ölçülür. Enerji verimliliği açısından önemlidir.",
				CategoryId = 1,
				Created = DateTime.Now,
				IsDeleted = false,
			};

			Detail detail4 = new Detail()
			{
				Id = 4,
				Title = "İşlemci (CPU)",
				Description = "Bilgisayarın temel işlem gücünü belirler. Program derleme, uygulama çalıştırma gibi işlemlerde performansı etkiler. Örn: Intel Core i7, AMD Ryzen 5.",
				CategoryId = 2,
				Created = DateTime.Now,
				IsDeleted = false,
			};

			Detail detail5 = new Detail()
			{
				Id = 5,
				Title = "RAM (Bellek)",
				Description = "Aynı anda çalıştırılan uygulamaların hızlı çalışmasını sağlar. 8 GB ve üzeri RAM, yazılım geliştirme için idealdir.",
				CategoryId = 2,
				Created = DateTime.Now,
				IsDeleted = false,
			};

			Detail detail6 = new Detail()
			{
				Id = 6,
				Title = "Ekran Kartı (GPU)",
				Description = "Grafik işlem görevlerini yerine getirir. Oyun, video işleme ve bazı yapay zeka uygulamalarında kullanılır. Geliştiriciler için genellikle entegre GPU yeterlidir.",
				CategoryId = 2,
				Created = DateTime.Now,
				IsDeleted = false,
			};

			builder.HasData(detail1, detail2, detail3, detail4, detail5, detail6);
		}
	}
}
