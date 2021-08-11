using ApiGateway.BusinessLayer.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace ApiGateway.WebApi.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ExceptionFilter> _logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, context.Exception.Message);
            context.ExceptionHandled = true;

            if (context.Exception is ApiBadRequestException)
            {
                context.Result = new BadRequestObjectResult(context.Exception.Message);
            }
            else
            {
                context.Result = new BadRequestObjectResult(context.Exception.Message);
            }
        }
    }
}
