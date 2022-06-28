using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Reflection;

namespace EmployeeArrivalTrackerClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).ConfigureLogging((hostingContext, logging) =>
                {
                    var assembly = Assembly.GetAssembly(typeof(Program));
                    var fullConfigFilePath = Path.Combine(hostingContext.HostingEnvironment.ContentRootPath,
                                                          "log4net.config");

                    var logEmployeeArrival = LogManager.GetRepository(assembly);
                    XmlConfigurator.Configure(logEmployeeArrival, new FileInfo(fullConfigFilePath));
                });
    }
}
