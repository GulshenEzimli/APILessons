using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;

namespace Persistence
{
	public static class Registration
	{
		public static void AddContext(this IServiceCollection services, string connectionString)
		{
			services.AddDbContext<ApiContext>(options => options.UseSqlServer(connectionString));
		}
	}
}
