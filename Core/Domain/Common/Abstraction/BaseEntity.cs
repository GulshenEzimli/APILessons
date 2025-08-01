using Domain.Common.Interface;

namespace Domain.Common.Abstraction
{
	public class BaseEntity : IEntity
	{
		public int Id { get; set; }
		public DateTime Created { get; set; }
		public bool IsDeleted { get; set; }
	}
}
