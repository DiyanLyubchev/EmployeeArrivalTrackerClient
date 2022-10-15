using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading.Tasks;
using System.Threading;
using EmployeeArrivalTrackerDataAccess.Context;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeArrivalTrackerDomain.HealthCheck
{
    public class DbConnectionHealthCheck : IHealthCheck
    {
        private readonly IServiceProvider serviceProvider;

        public DbConnectionHealthCheck(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            using var scope = this.serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<EmployeeArrivalContext>();

            bool doesDatabaseConnect = dbContext.Database.CanConnect();
            if (doesDatabaseConnect)
            {
                return Task.FromResult(HealthCheckResult.Healthy("Db connection completed."));
            }

            return Task.FromResult(HealthCheckResult.Unhealthy("Db connection faild."));
        }
    }
}
