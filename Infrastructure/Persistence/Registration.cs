using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;

namespace Persistence
{
    public static class Registration
    {
        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionstring = configuration.GetSection("ConnectionStrings:Default").Value;
            services.AddDbContext<ApiLessonsDbContext>(options => options.UseSqlServer(connectionstring));

        }
    }
}
