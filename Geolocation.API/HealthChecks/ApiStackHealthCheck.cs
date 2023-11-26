using Geolocation.Domain.Abstrations;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Geolocation.Infrastructure.Healthchecks
{
    public class ApiStackHealthCheck : IHealthCheck
    {
        private readonly IApiStackClient _client;

        public ApiStackHealthCheck(IApiStackClient client)
        {
            _client = client;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                await _client.CheckAsync(cancellationToken);
                return HealthCheckResult.Healthy();
            }
            catch(Exception ex)
            {
                return HealthCheckResult.Unhealthy(exception: ex);
            }
        }
    }
}
