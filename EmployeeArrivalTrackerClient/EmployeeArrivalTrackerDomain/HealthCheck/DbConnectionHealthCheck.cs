using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading.Tasks;
using System.Threading;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EmployeeArrivalTrackerDomain.HealthCheck
{
    public class DbConnectionHealthCheck<TContext> : IHealthCheck where TContext : DbContext
    {
        private readonly IServiceProvider serviceProvider;

        public DbConnectionHealthCheck(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context, CancellationToken cancellationToken = default)

        {
            Type type = typeof(TContext);
            bool doesDatabaseConnect = false;
            using (var scope = serviceProvider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var scopeDbContext = services.GetService<TContext>();

                doesDatabaseConnect = await scopeDbContext.Database.CanConnectAsync(cancellationToken);
            }
         
            if (doesDatabaseConnect)
            {
                return HealthCheckResult.Healthy($"{type.Name} connection completed - {context.Registration.Name}");
            }

            return HealthCheckResult.Unhealthy($"{type.Name} connection faild - {context.Registration.Name}");
        }
    }
}
