using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Geolocation.Infrastructure.Healthchecks
{
    public class DatabaseHealthCheck : IHealthCheck
    {
        private readonly DataContext _dataContext;

        public DatabaseHealthCheck(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                await _dataContext.Database.ExecuteSqlRawAsync("SELECT COUNT(*) FROM [dbo].[Location]", cancellationToken);
                return HealthCheckResult.Healthy();
            }
            catch(Exception ex)
            {
                return HealthCheckResult.Unhealthy(exception: ex);
            }
        }
    }
}
