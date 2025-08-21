using Persistence;
using Application;
namespace LessonsWebApi
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var env = builder.Environment;
			builder.Configuration.SetBasePath(env.ContentRootPath);
			builder.Configuration.AddJsonFile("appsettings.json", optional: false);
			builder.Configuration.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
			
			var connectionString = builder.Configuration.GetSection("ConnectionStrings").GetSection("Default").Value;
			builder.Services.AddContext(connectionString);
			builder.Services.AddPersistence();
			builder.Services.AddApplication();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
