using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
	public class Role : IdentityRole<Guid>
	{
		public Guid Id { get; set; }
	}
}
