using Census.API.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Census.API.Infrastructure.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionHandlingMiddleware> logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (InvalidInputException ex)
            {
                await HandleInvalidInputExceptionAsync(httpContext, ex);
            }
            catch (Exception ex)
            {             
                await HandleUnknownExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleInvalidInputExceptionAsync(HttpContext context, Exception exception)
        {
            logger.LogError($"Internal Error: {exception}");

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            return context.Response.WriteAsync(
                JsonConvert.SerializeObject(
                new
                {
                    StatusCode = context.Response.StatusCode,
                    Message = exception.Message
                }));
        }

        private Task HandleUnknownExceptionAsync(HttpContext context, Exception exception)
        {
            logger.LogError($"Internal Error: {exception}");

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(
                JsonConvert.SerializeObject(
                new
                {
                    StatusCode = context.Response.StatusCode,
                    Message = "Internal Server Error."
                }));
        }
    }
}
