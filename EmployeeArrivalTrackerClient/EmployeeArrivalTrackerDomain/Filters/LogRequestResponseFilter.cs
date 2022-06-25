using Common.Models.Producer;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeArrivalTrackerDomain.Filters
{
    public class LogRequestResponseFilter : IAsyncActionFilter
    {
        private readonly ILogger<ValidationFilter> logger;

        public LogRequestResponseFilter(ILogger<ValidationFilter> logger)
        {
            this.logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Lazy<ICollection<object>> location = new(() => GetLocation(context));

            LogRequest(context, location);
            await next();
            LogResponse(context, location);
        }

        private void LogRequest(ActionExecutingContext context, Lazy<ICollection<object>> location)
        {
            if (location.Value.Contains("Produce"))
            {
                var request = context.ActionArguments["data"] as List<ProducerArrivalEmployeesVM>;

                if (request is not null)
                {
                    this.logger.LogInformation($"Arrival Employees are: {request.Count}");
                }
            }
        }

        private void LogResponse(ActionExecutingContext context, Lazy<ICollection<object>> location)
        {
            this.logger.LogInformation($"Response code is: {context.HttpContext.Response.StatusCode} " +
                $"Controller: {location.Value.First()}  Action: {location.Value.Last()}");
        }

        private ICollection<object> GetLocation(ActionExecutingContext context)
        {
            return context.RouteData.Values.Values;
        }
    }
}
