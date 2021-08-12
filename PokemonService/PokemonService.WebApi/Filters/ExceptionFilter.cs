﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PokemonService.DataAccess.Exceptions;

namespace PokemonService.WebApi.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ExceptionFilter> _logger;
        private readonly IWebHostEnvironment _environment;

        public ExceptionFilter(ILogger<ExceptionFilter> logger, IWebHostEnvironment env)
        {
            _logger = logger;
            _environment = env;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, context.Exception.Message);
            context.ExceptionHandled = true;

            if (context.Exception is EntityNotFoundException)
            {
                context.Result = new BadRequestObjectResult(context.Exception.Message);
            }
            else if (context.Exception is UniqueIndexViolationException)
            {
                context.Result = new BadRequestObjectResult(context.Exception.Message);
            }
            else if (_environment.IsDevelopment())
            {
                context.Result = new BadRequestObjectResult(context.Exception.Message);
            }
            else
            {
                context.Result = new BadRequestObjectResult("Internal server error");
            }
        }
    }
}
