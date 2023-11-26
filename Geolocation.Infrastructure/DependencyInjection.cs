using Geolocation.API.Clients.ApiStack;
using Geolocation.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Geolocation.Infrastructure 
{ 
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<DataContext>();
            services.AddScoped<IUnitOfWork>(x => x.GetService<DataContext>());
            services.AddScoped<IGeolocalizationRepository, GeolocalizationRepository>();

            var apiStackConf = configuration.GetSection(nameof(ApiStackConfiguration)).Get<ApiStackConfiguration>();

            services.AddSingleton<IApiStackConfiguration>(x => apiStackConf);
            services.AddSingleton<IApiStackClient, ApiStackClient>();
            services.AddHttpClient<IApiStackClient, ApiStackClient>(client => client.BaseAddress = new Uri(apiStackConf.Address));

            return services;
        }
    }
}