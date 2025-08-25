using Application.Bases;
using Application.Behaviors;
using Application.Exceptions;
using Application.Features.Rules;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Reflection;

namespace Application
{
	public static class Registration
	{
		public static void AddApplication(this IServiceCollection services)
		{
			var assembly = Assembly.GetExecutingAssembly();

			services.AddTransient<ExceptionMiddleware>();

			services.AddMediatR(config => config.RegisterServicesFromAssembly(assembly));

			services.AddValidatorsFromAssembly(assembly);
			ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("az");

			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehavior<,>));

			services.AddAllRulesFromAssemblyContaining(assembly, typeof(BaseRules));
		}
		private static void AddAllRulesFromAssemblyContaining(this IServiceCollection services, Assembly assembly, Type type)
		{
			var types = assembly.GetTypes().Where(t => t.IsSubclassOf(type) && t != type).ToList();

			types.ForEach(ruleType => services.AddTransient(ruleType));
		}
	}
}
