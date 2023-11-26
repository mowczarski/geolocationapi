using GeolocationAPI.Authentication;
using GeolocationAPI.Clients;
using GeolocationAPI.Clients.Contracts;
using GeolocationAPI.EF;
using GeolocationAPI.EF.Repositories;
using GeolocationAPI.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;

namespace GeolocationAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddAndConfigureSwagger();

            builder.Services.AddSingleton<IApiStackClient, ApiStackClient>();
            builder.Services.AddHttpClient<IApiStackClient, ApiStackClient>(
                client => client.BaseAddress = new Uri(builder.Configuration.GetSection(nameof(ApiStackConfiguration)).Get<ApiStackConfiguration>().Address));

            builder.Services.AddScoped<DataContext>();
            builder.Services.AddScoped<IUnitOfWork>(x => x.GetService<DataContext>());
            builder.Services.AddScoped<GeolocationRepository>();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("logs/apilog.txt", rollingInterval: RollingInterval.Day)
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
