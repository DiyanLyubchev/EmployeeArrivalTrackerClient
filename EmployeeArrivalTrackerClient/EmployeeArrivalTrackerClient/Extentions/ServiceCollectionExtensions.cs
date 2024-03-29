﻿using EmployeeArrivalTrackerDataAccess.Context;
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
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Prometheus;
using System.Reflection;
using System;
using System.Linq;

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
            });

            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<ProducerArrivalEmployeesVMValidator>();
        }

        public static void ResolveHealthCheck(this IServiceCollection services)
        {
            services.AddTransient<DbConnectionHealthCheck<EmployeeArrivalContext>>();

            //Second is for testing
            services.AddHealthChecks().AddCheck<DbConnectionHealthCheck<EmployeeArrivalContext>>("DbConnection 1", tags: new[] { "ready" })
                                      .AddCheck<DbConnectionHealthCheck<EmployeeArrivalContext>>("DbConnection 2", tags: new[] { "ready" })
                                      .AddCheck("Service Register", () => {
                                          Assembly assembly = GetAssemblyByName("EmployeeArrivalTrackerDomain");
                                          Type[] types = assembly.GetTypes()
                                                                 .Where(x => x.Name.StartsWith("I") && x.Name.EndsWith("Manager"))
                                                                 .ToArray();

                                          var serviceProvider = services.BuildServiceProvider();
                                          foreach (var item in types)
                                          {
                                              var service = serviceProvider.GetService(item);

                                              if (service == null)
                                              {
                                                  return HealthCheckResult.Unhealthy("Unhealthy service registration");
                                              }
                                          }

                                          return HealthCheckResult.Healthy("Healthy service registration");
                                      }, tags: new[] { "ready" })
                                      .ForwardToPrometheus();

        }

        static Assembly GetAssemblyByName(string name)
        {
            return AppDomain.CurrentDomain.GetAssemblies().
                   SingleOrDefault(assembly => assembly.GetName().Name == name);
        }
    }
}
