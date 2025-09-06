using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
	public class User : IdentityUser<Guid>
	{
		public Guid Id { get; set; }
		public string FullName { get; set; }
		public string? RefreshToken { get; set; }
		public DateTime? RefreshTokenExpiryTime { get; set; }
	}
}
