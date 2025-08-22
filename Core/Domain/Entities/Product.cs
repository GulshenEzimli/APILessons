using Domain.Common.Abstraction;

namespace Domain.Entities
{
	public class Product : BaseEntity
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public decimal Discount { get; set; }
		public int BrandId { get; set; }
		public Brand Brand { get; set; }
		public IList<CategoryProduct> ProductCategories { get; set; }
	}
}
