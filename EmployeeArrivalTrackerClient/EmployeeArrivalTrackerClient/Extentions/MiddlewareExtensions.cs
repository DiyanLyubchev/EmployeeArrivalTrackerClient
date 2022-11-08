using Common.Models.HealthCheck;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Prometheus;
using System;
using System.Linq;
using System.Net.Mime;

namespace EmployeeArrivalTrackerClient.Extentions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder ResolveEndpoints(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                          name: "default",
                          pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapHealthChecks("/metrics");

                endpoints.MapHealthChecks("/health/live", new HealthCheckOptions
                {
                    Predicate = _ => false,
                    ResultStatusCodes =
                    {
                        [HealthStatus.Healthy] = StatusCodes.Status200OK,
                        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
                    }
                });

                endpoints.MapHealthChecks("/health/ready", new HealthCheckOptions
                {
                    Predicate = healthCheck => healthCheck.Tags.Contains("ready"),
                    ResponseWriter = async (context, report) =>
                    {
                        string result = PrepareResponse(report);
                        context.Response.ContentType = MediaTypeNames.Application.Json;
                        await context.Response.WriteAsync(result);
                    },
                    ResultStatusCodes =
                    {
                        [HealthStatus.Healthy] = StatusCodes.Status200OK,
                        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
                    }
                });
            });

            return app;
        }

        private static string PrepareResponse(HealthReport report)
        {
            return JsonConvert.SerializeObject(                                          
                new HealthResult                                          
                {                                          
                    Name = "Employee arrival tracker",                                          
                    Status = report.Status.ToString(),                                          
                    Duration = report.TotalDuration,                                          
                    Info = report.Entries.Select(healthReportEntry => new HealthInfo                                          
                    {                                          
                        Key = healthReportEntry.Key,                                          
                        Description = healthReportEntry.Value.Description,                                         
                        Duration = healthReportEntry.Value.Duration,                                         
                        Status = Enum.GetName(typeof(HealthStatus),                                          
                        healthReportEntry.Value.Status),                                          
                        Error = healthReportEntry.Value.Exception?.Message                                         
                    }).ToList()                                         
                },                                         
                Formatting.Indented,                                         
                new JsonSerializerSettings                                          
                {                                          
                    NullValueHandling = NullValueHandling.Ignore                                          
                });
        }
    }
}

