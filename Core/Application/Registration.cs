using Application.Interfaces.AutoMapper;
using Application.Mapping;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class Registration
    {
        public static void AddApplication(this IServiceCollection services)
        {
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(currentAssembly));

            services.AddSingleton<ICustomMapper, Mapper>(); 
        }
    }
}
