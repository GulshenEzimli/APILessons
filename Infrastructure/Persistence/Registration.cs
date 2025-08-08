using Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repositories;

namespace Persistence
{
	public static class Registration
	{
		public static void AddContext(this IServiceCollection services, string connectionString)
		{
			services.AddDbContext<ApiContext>(options => options.UseSqlServer(connectionString));
		}

		public static void AddPersistence(this IServiceCollection services)
		{
			services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
		}
	}
}
