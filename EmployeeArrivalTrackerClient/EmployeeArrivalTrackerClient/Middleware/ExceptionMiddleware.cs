using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace EmployeeArrivalTrackerClient.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                Console.WriteLine("Service request");
                await this.next(httpContext);
                Console.WriteLine("Service response");
            }
            catch (Exception ex)
            {
                httpContext.Session.Clear();
                this.logger.LogError($"Exception {ex.InnerException?.Message } { ex.Message } {Environment.NewLine} Location: {ex.StackTrace}");
                string errorMsg = ex.InnerException?.Message ?? ex.Message;
                httpContext.Session.SetString("Error", $"{errorMsg} ");
                httpContext.Response.Redirect("/home/error");
            }
        }
    }
}
