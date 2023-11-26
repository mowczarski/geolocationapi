using Microsoft.Extensions.DependencyInjection;
using Geolocation.Application.Abstraction.Logging;

namespace Geolocation.Application 
{

    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
                configuration.AddOpenBehavior(typeof(Logging<,>));
            });

            return services;
        }
    }
}