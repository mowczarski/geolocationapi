using GeolocationAPI.Authentication;
using GeolocationAPI.Clients;
using GeolocationAPI.Clients.Contracts;
using GeolocationAPI.EF;
using GeolocationAPI.EF.Repositories;
using GeolocationAPI.Models;
using GeolocationAPI.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;

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

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<AuthMiddleware>();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
