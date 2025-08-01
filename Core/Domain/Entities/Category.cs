using Domain.Common.Abstraction;

namespace Domain.Entities
{
	public class Category : BaseEntity
	{
		public string Name { get; set; }
		public int Priority { get; set; }
		public int ParentId { get; set; }
		public IList<Detail> Deatils { get; set; }
	}
}
