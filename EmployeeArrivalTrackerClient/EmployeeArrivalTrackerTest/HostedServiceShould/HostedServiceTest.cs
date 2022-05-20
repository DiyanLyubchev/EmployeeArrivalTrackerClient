using EmployeeArrivalTrackerDomain.HostedService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeArrivalTrackerTest.HostedServiceShould
{
    [TestClass]
    public class HostedServiceTest
    {
        [TestMethod]
        public async Task EmployeeArrivalHostedService_Run_Test()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddHostedService<EmployeeArrivalHostedService>();

            var serviceProvider = services.BuildServiceProvider();

            var service = serviceProvider.GetService<IHostedService>() as EmployeeArrivalHostedService;

            var isExecuted = false;
            if (service.StartAsync(CancellationToken.None).IsCompleted)
            {
                isExecuted = true;
            }
            await Task.Delay(100);
            Assert.IsTrue(isExecuted);
        }

        [TestMethod]
        public async Task EmployeeArrivalHostedService_Stop_Test()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddHostedService<EmployeeArrivalHostedService>();

            var serviceProvider = services.BuildServiceProvider();

            var service = serviceProvider.GetService<IHostedService>() as EmployeeArrivalHostedService;

            var isExecuted = false;
            if (service.StopAsync(CancellationToken.None).IsCompleted)
            {
                isExecuted = true;
            }
            await Task.Delay(100);
            Assert.IsTrue(isExecuted);
        }
    }
}
