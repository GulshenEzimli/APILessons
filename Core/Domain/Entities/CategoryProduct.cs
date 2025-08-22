using Domain.Common.Abstraction;
using Domain.Common.Interface;

namespace Domain.Entities
{
	public class CategoryProduct : IEntity
	{
		public int ProductId { get; set; }
		public int CategoryId { get; set; }
		public Product Product { get; set; }
		public Category Category { get; set; }
	}
}
