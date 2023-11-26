using Geolocation.Infrastructure;
using Geolocation.API.Middleware;
using Geolocation.API.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Geolocation.Application;
using Microsoft.Extensions.Configuration;

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

            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(builder.Configuration);
            
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(builder.Configuration.GetValue<string>("Serilog:FilePath"), rollingInterval: RollingInterval.Day)
                .CreateLogger();
            
            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.UseMiddleware<AuthMiddleware>();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
