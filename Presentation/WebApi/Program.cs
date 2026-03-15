using Persistence;
namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            var env = builder.Environment;
            builder.Configuration.SetBasePath(env.ContentRootPath)
                               .AddJsonFile("appSettings.json", optional : false)
                               .AddJsonFile($"appSettings.{env.EnvironmentName}.json", optional : true);

            builder.Services.AddDbContext(builder.Configuration);
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
