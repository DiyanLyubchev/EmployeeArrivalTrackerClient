using EmployeeArrivalTrackerDomain.Contracts;
using EmployeeArrivalTrackerInfrastructure.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeArrivalTrackerDomain.HostedService
{
    public sealed class EmployeeArrivalHostedService : IHostedService, IDisposable
    {
        private Timer timer;
        private readonly IServiceProvider serviceProvider;

        public EmployeeArrivalHostedService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            this.timer = new Timer(CallService, null, TimeSpan.Zero,
                TimeSpan.FromHours(12));

            return Task.CompletedTask;
        }

        private async void CallService(object state)
        {
            using (var scope = this.serviceProvider.CreateScope())
            {
                var clientService = scope.ServiceProvider.GetRequiredService<IClientsService>();
                var tokenManager = scope.ServiceProvider.GetRequiredService<ITokenManager>();

                try
                {
                    string response = await clientService.CallCliensServiceAsync();
                    tokenManager.AddTokenData(response);
                }
                catch (Exception)
                {
                }
            };
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            this.timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            this.timer?.Dispose();
        }
    }
}
