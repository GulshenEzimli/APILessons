using Application;
using Application.Exceptions;
using Infrastructure;
using Microsoft.AspNetCore.Builder;
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
            //builder.Services.AddOpenApi();
            //builder.Services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo() { Version = "v1", Title = "Learn API", Description = "API with JWTBearer authentication"});
            //    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            //    {
            //        Name = "Authorization",
            //        BearerFormat = "JWT",
            //        Scheme = "Bearer",
            //        Type = SecuritySchemeType.ApiKey,
            //        In = ParameterLocation.Header,
            //        Description = "Bearer <token> kimi tokeni elave ede bilersiniz."
            //    });
            //    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            //    {
            //        {
            //            new OpenApiSecurityScheme()
            //            {
            //                Reference = new OpenApiReference()
            //                {
            //                    Id = "Bearer",
            //                    Type = ReferenceType.SecurityScheme
            //                }
            //            },
            //            Array.Empty<string>()
            //        }
            //    });
            //});
            builder.Services.AddSwaggerGen();

            var env = builder.Environment;
            builder.Configuration.SetBasePath(env.ContentRootPath)
                               .AddJsonFile("appSettings.json", optional : false)
                               .AddJsonFile($"appSettings.{env.EnvironmentName}.json", optional : true);

            builder.Services.AddPersistence(builder.Configuration);
            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(builder.Configuration);

            var app = builder.Build();

            //if (app.Environment.IsDevelopment())
            //{
            //    app.MapOpenApi();
            //}
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
