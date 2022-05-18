using EmployeeArrivalTrackerClient.Middleware;
using Microsoft.AspNetCore.Builder;

namespace EmployeeArrivalTrackerClient.Extentions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
