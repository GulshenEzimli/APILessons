using Application;
using Application.Exceptions;
using Infrastructure;
using Microsoft.OpenApi.Models;
using Persistence;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo() { Title = "Create API", Version = "v1", Description = "Create API with JWTBearer."});
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme(){
                    Name = "Authorization",
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Type = SecuritySchemeType.Http,
                    In = ParameterLocation.Header,
                    Description = "Bearer <token> kimi token daxil edin."
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
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
            builder.Configuration.SetBasePath(env.ContentRootPath)
                               .AddJsonFile("appSettings.json", optional : false)
                               .AddJsonFile($"appSettings.{env.EnvironmentName}.json", optional : true);

            builder.Services.AddPersistence(builder.Configuration);
            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(builder.Configuration);

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseExceptionMiddleware();
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
