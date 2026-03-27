using Application.Interfaces.Repositories.EfCore;
using Application.Interfaces.UnitOfWorks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using Persistence.Repositories.EfCore;
using Persistence.UnitOfWorks;

namespace Persistence
{
    public static class Registration
    {
        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionstring = configuration.GetSection("ConnectionStrings:Default").Value;
            services.AddDbContext<ApiLessonsDbContext>(options => options.UseSqlServer(connectionstring));

            services.AddIdentityCore<User>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.SignIn.RequireConfirmedEmail = false;
            })
                .AddRoles<Role>()
                .AddEntityFrameworkStores<ApiLessonsDbContext>();
        }

        public static void AddPersistence(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
        }
    }
}
