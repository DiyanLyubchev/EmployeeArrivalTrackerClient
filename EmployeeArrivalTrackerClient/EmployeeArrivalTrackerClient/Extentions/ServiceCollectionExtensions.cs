using EmployeeArrivalTrackerDataAccess.Context;
using EmployeeArrivalTrackerDataAccess.Contracts;
using EmployeeArrivalTrackerDataAccess.DbManager;
using EmployeeArrivalTrackerDomain.Application;
using EmployeeArrivalTrackerDomain.Contracts;
using EmployeeArrivalTrackerDomain.Filters;
using EmployeeArrivalTrackerDomain.Validators;
using EmployeeArrivalTrackerInfrastructure;
using EmployeeArrivalTrackerInfrastructure.Contracts;
using FluentValidation.AspNetCore;
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

        public static void ResolveControllersOptions(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
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
    }
}
