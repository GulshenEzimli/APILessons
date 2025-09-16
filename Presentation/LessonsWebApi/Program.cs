using Persistence;
using Application;
using Application.Exceptions;
using Microsoft.OpenApi.Models;
namespace LessonsWebApi
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(opt =>
			{
				opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Youtube Api", Version = "v1", Description = "Youtube Api Lessons" });
				opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
				{
					Name = "Authorization",
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer",
					BearerFormat = "JWT",
					Description = "Bearer yazıb boşluq işarəsi qoyduqdan sonra tokeni daxil edin",
					In = ParameterLocation.Header
				});
				opt.AddSecurityRequirement(new OpenApiSecurityRequirement()
				{
					{
						new OpenApiSecurityScheme()
						{
							Reference = new OpenApiReference()
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							}
						},
						Array.Empty<string>()
					}
				});
			});

			var env = builder.Environment;
			builder.Configuration.SetBasePath(env.ContentRootPath);
			builder.Configuration.AddJsonFile("appsettings.json", optional: false);
			builder.Configuration.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

			var connectionString = builder.Configuration.GetSection("ConnectionStrings").GetSection("Default").Value;
			builder.Services.AddContext(connectionString);
			builder.Services.AddPersistence();
			builder.Services.AddApplication();
			builder.Services.AddCustomMapper();
			builder.Services.AddHttpContextAccessor();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.ConfigureExceptionHandlingMiddleware();
			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}
	}
}
