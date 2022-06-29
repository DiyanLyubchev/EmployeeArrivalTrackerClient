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
            ICollection<object> location = GetLocation(context);

            LogRequest(context, location);
            await next();
            LogResponse(context, location);
        }

        private void LogRequest(ActionExecutingContext context, ICollection<object> location)
        {
            Lazy<IDictionary<string, object>> parameterFromAction = new(() => GetParameter(context));

            if (location.Contains("Produce"))
            {
                //var request = parameterFromAction.Value["data"] as List<ProducerArrivalEmployeesVM>;
                //if (request is not null)
                //{
                //    this.logger.LogInformation($"Arrival Employees are: {request.Count}");
                //}

                //Pattern Matching -> same as the above example
                if (parameterFromAction.Value["data"] is List<ProducerArrivalEmployeesVM> request)
                {
                    this.logger.LogInformation($"Arrival Employees are: {request.Count}");
                }
            }
        }

        private void LogResponse(ActionExecutingContext context, ICollection<object> location)
        {
            this.logger.LogInformation($"Response code is: {context.HttpContext.Response.StatusCode} " +
                $"Controller: {location.First()}  Action: {location.Last()}");
        }

        private static ICollection<object> GetLocation(ActionExecutingContext context)
        {
            return context.RouteData.Values.Values;
        }

        private static IDictionary<string, object> GetParameter(ActionExecutingContext context)
        {
            return context.ActionArguments;
        }
    }
}
