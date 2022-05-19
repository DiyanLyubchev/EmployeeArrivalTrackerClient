using EmployeeArrivalTrackerDataAccess.Context;
using EmployeeArrivalTrackerDataAccess.Contracts;
using EmployeeArrivalTrackerDataAccess.DbManager;
using EmployeeArrivalTrackerDomain.Application;
using EmployeeArrivalTrackerDomain.Contracts;
using EmployeeArrivalTrackerInfrastructure;
using EmployeeArrivalTrackerInfrastructure.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeArrivalTrackerClient.Extentions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ResolveServices(this IServiceCollection services)
        {
            //Db Managers
            services.AddScoped<IEmployeeArrivalDbManager, EmployeeArrivalDbManager>();
            services.AddScoped<ITokenDbManager, TokenDbManager>();

            //Domain Managers
            services.AddScoped<IEmployeeArrivalManager, EmployeeArrivalManager>();
            services.AddScoped<ITokenManager, TokenManager>();

            //Infrastructure
            services.AddSingleton<IClientsService, ClientsService>();

            return services;
        }

        public static IServiceCollection ResolveContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EmployeeArrivalContext>(options =>
             options.UseSqlServer(
             configuration.GetConnectionString("DefaultConnection")));
            return services;
        }
    }
}
