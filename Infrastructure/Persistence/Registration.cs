using Application.Interfaces.Repositories;
using Application.Interfaces.Repositories.EfCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using Persistence.Repositories.EfCore;

namespace Persistence
{
    public static class Registration
    {
        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionstring = configuration.GetSection("ConnectionStrings:Default").Value;
            services.AddDbContext<ApiLessonsDbContext>(options => options.UseSqlServer(connectionstring));

        }

        public static void AddPersistence(this IServiceCollection services)
        {
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        }
    }
}
