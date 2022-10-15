using EmployeeArrivalTrackerDataAccess.Context;
using EmployeeArrivalTrackerDataAccess.Contracts;
using EmployeeArrivalTrackerDataAccess.Data;
using EmployeeArrivalTrackerDataAccess.DbManager;
using EmployeeArrivalTrackerDomain.Application;
using EmployeeArrivalTrackerDomain.Contracts;
using EmployeeArrivalTrackerDomain.Filters;
using EmployeeArrivalTrackerDomain.HealthCheck;
using EmployeeArrivalTrackerDomain.Validators;
using EmployeeArrivalTrackerInfrastructure;
using EmployeeArrivalTrackerInfrastructure.Contracts;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Prometheus;

namespace EmployeeArrivalTrackerClient.Extentions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ResolveServices(this IServiceCollection services)
        {
            //Db Managers
            services.AddScoped<IGenericRepository<Tokens>, GenericRepository<Tokens>>();
            services.AddScoped<IGenericRepository<EmployeeArrival>, GenericRepository<EmployeeArrival>>();
            services.AddScoped<IGenericRepository<EmployeeReport>, GenericRepository<EmployeeReport>>();

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

        public static void ResolveControllersOptions(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add<LogRequestResponseFilter>();
                options.Filters.Add<ValidationFilter>();
            })
            .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            })
            .AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssemblyContaining<ProducerArrivalEmployeesVMValidator>();
            });
        }

        public static void ResolveHealthCheck(this IServiceCollection services)
        {
            services.AddHostedService<StartupBackgroundService>();
            services.AddSingleton<StartupHealthCheck>();
            services.AddSingleton<DbConnectionHealthCheck>();

            services.AddHealthChecks().AddCheck<StartupHealthCheck>("Startup", tags: new[] { "ready" })
                                      .AddCheck<DbConnectionHealthCheck>("DbConnection", tags: new[] { "live-db" })
                                      .ForwardToPrometheus();
        }
    }
}
