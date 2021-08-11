using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace ApiGateway.WebApi.Filters
{
    public class ActivityLoggerFilter : IActionFilter
    {
        private readonly ILogger<ActivityLoggerFilter> _logger;

        public ActivityLoggerFilter(ILogger<ActivityLoggerFilter> logger)
        {
            _logger = logger;
        }

        void IActionFilter.OnActionExecuted(ActionExecutedContext context)
        {
            var message = $"Action executed '{context.ActionDescriptor.DisplayName}'." +
                $"TraceId {context.HttpContext.TraceIdentifier}.";
            _logger.LogInformation(message);

            message = $"Response status code '{context.HttpContext.Response.StatusCode}'." +
                $"TraceId '{context.HttpContext.TraceIdentifier}'.";
            _logger.LogInformation(message);
        }

        void IActionFilter.OnActionExecuting(ActionExecutingContext context)
        {
            var message = $"Executing action '{context.ActionDescriptor.DisplayName}'." +
                $"TraceId '{context.HttpContext.TraceIdentifier}'.";
            _logger.LogInformation(message);

            message = $"Content length '{context.HttpContext.Request.ContentLength}'." +
                $"TraceId '{context.HttpContext.TraceIdentifier}'.";
            _logger.LogInformation(message);
        }
    }
}
