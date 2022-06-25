using Common.Models.Error;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeArrivalTrackerDomain.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        private readonly ILogger<ValidationFilter> logger;

        public ValidationFilter(ILogger<ValidationFilter> logger)
        {
            this.logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var response = CreateResponseModel(context);

                context.Result = new BadRequestObjectResult(response);
                return;
            }

            await next();
        }

        private ErrorViewModel CreateResponseModel(ActionExecutingContext context)
        {

            ErrorViewModel response = new();

            response.ErrorMsg = string.Join(Environment.NewLine, context.ModelState.Values.Where(e => e.Errors.Count > 0)
                         .SelectMany(E => E.Errors)
                         .Select(E => E.ErrorMessage)
                         .ToList());

            LogErrors(response);

            return response;

        }

        private bool LogErrors(ErrorViewModel response)
        {
            this.logger.LogError("Validation error {dateTime} {reason}", DateTime.Now, response.ErrorMsg);

            return true;
        }
    }
}
