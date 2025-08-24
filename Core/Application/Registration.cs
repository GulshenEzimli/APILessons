using Application.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
	public static class Registration
	{
		public static void AddApplication(this IServiceCollection services)
		{
			services.AddTransient<ExceptionMiddleware>();

			services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
		}
	}
}
