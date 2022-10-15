using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Prometheus;

namespace EmployeeArrivalTrackerClient.Extentions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseWebService(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMetricServer();//Starting the metrics exporter, will expose "/metrics"
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseSession();
            app.ConfigureExceptionHandler();

            app.UseRouting();

            ////adding metrics related to HTTP
            app.UseHttpMetrics(options =>
            {
                options.AddCustomLabel("host", context => context.Request.Host.Host);
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                          name: "default",
                          pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapHealthChecks("/metrics");

                endpoints.MapHealthChecks("/healthz/ready", new HealthCheckOptions
                {
                    Predicate = healthCheck => healthCheck.Tags.Contains("ready")
                });

                endpoints.MapHealthChecks("/healthz/live-db", new HealthCheckOptions
                {
                    Predicate = healthCheck => healthCheck.Tags.Contains("live-db")
                });
            });

            return app;
        }
    }
}

