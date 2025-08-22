using Application.Interfaces.AutoMappers;
using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.AutoMapper;
using Persistence.Contexts;
using Persistence.Repositories;
using Persistence.UnitOfWorks;

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
			services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));

			services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
		}

		public static void AddCustomMapper(this IServiceCollection services)
		{
			services.AddSingleton<IMapper, Mapper>();
		}
	}
}
