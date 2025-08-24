using Application.Behaviors;
using Application.Exceptions;
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
			services.AddTransient<ExceptionMiddleware>();

			services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

			services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
			ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("az");

			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehavior<,>));
		}
	}
}
