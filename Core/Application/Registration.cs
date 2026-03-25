using Application.Bases;
using Application.Behaviors;
using Application.Exceptions;
using Application.Interfaces.AutoMapper;
using Application.Mapping;
using FluentValidation;
using MediatR;
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
            services.AddTransient<ExceptionMiddleware>();

            services.AddValidatorsFromAssembly(currentAssembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehavior<,>));

            services.AddRulesFromAssemblyContaining(currentAssembly, typeof(BaseRules));

        }
        private static IServiceCollection AddRulesFromAssemblyContaining(this IServiceCollection services, Assembly assembly, Type type)
        {
            var ruleTypes = assembly.GetTypes().Where(t => t.IsSubclassOf(type) && t != type).ToList();

            foreach (var item in ruleTypes)
            {
                services.AddTransient(item);
            }

            return services;
        }
    }
}
