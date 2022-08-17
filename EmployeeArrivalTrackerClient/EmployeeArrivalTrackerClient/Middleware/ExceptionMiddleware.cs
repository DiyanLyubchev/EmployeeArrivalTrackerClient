using Common;
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
                this.logger.LogInformation($"Service request time: {DateTime.Now:dd.MM.yyyy hh:mm:ss}");

                await this.next(httpContext);

                this.logger.LogInformation($"Service response time: {DateTime.Now:dd.MM.yyyy hh:mm:ss}");
            }
            catch (Exception ex)
            {
               
                httpContext.Session.Clear(); 

                this.logger.LogError($"Env: {Utils.GetCurrentEnvironment()} {Environment.NewLine} Exception {ex.InnerException?.Message } { ex.Message } {Environment.NewLine} Location: {ex.StackTrace}");
                string errorMsg = ex.InnerException?.Message ?? ex.Message;
                httpContext.Session.SetString("Error", $"{errorMsg} ");
                httpContext.Response.Redirect("/home/error");
            }
        }
    }
}
