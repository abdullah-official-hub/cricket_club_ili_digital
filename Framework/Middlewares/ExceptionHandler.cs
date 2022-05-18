using Framework.Exceptions;
using Framework.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Framework.Middlewares
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;

        public ExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (StatusCodeException statusCodeException)
            {
                await HandleStatusCodeException(context, statusCodeException);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private static Task HandleStatusCodeException(HttpContext context, StatusCodeException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)exception.HttpStatusCode;
            var exceptionResponse = new ExceptionResponse()
            {
                Message = exception.Message,
                Type = exception.Type
            };
            return context.Response.WriteAsync(JsonConvert.SerializeObject(exceptionResponse));
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var exceptionResponse = new ExceptionResponse()
            {
                Message = "Internal server error.",
                Type = "Unknown"
            };
            return context.Response.WriteAsync(JsonConvert.SerializeObject(exceptionResponse));
        }
    }
}