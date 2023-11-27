using Geolocation.Infrastructure;
using Geolocation.API.Middleware;
using Geolocation.API.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Geolocation.Application;
using Microsoft.Extensions.Configuration;
using FluentValidation.AspNetCore;
using System.Reflection;
using Geolocation.Infrastructure.Healthchecks;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using Serilog;
using Geolocation.API.Controllers.Helpers;

namespace Geolocation.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddAndConfigureSwagger();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader());
            });

            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(builder.Configuration);

            builder.Services.AddFluentValidation(c => c.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));

            builder.Services.AddHealthChecks()
                .AddCheck<DatabaseHealthCheck>("Database")
                .AddCheck<ApiStackHealthCheck>("ApiStack");

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(builder.Configuration.GetValue<string>("Serilog:FilePath"), rollingInterval: RollingInterval.Day)
                .CreateLogger();
            
            var app = builder.Build();

            app.MapHealthChecks(Endpoints.Healthchecks, new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.UseMiddleware<AuthMiddleware>();
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseCors("AllowAll");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
