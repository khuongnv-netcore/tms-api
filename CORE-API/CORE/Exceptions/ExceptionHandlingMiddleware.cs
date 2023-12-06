using CORE_API.CORE.Services.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace CORE_API.CORE.Exceptions
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogService _logService;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogService logService)
        {
            _next = next;
            _logService = logService;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.ContentType = "application/json";
            var code = HttpStatusCode.InternalServerError;
            string result = "";

            if (exception is ApiBadRequestException)
            {
                code = HttpStatusCode.BadRequest;
            }

            if (exception is ApiNotFoundException)
            {
                code = HttpStatusCode.NotFound;
            }
            if(exception is ApiDatabaseException)
            {
                code = HttpStatusCode.InternalServerError;
            }

            // log the exception
            try
            {
                _logService.LogException(exception);
            }
            catch (Exception e)
            {
               //Fail Silently
               result = System.Text.Json.JsonSerializer.Serialize(new {error=e.Message});                
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return httpContext.Response.WriteAsync(result);
            }

            result = System.Text.Json.JsonSerializer.Serialize(new ApiExceptionInfo(exception));
            httpContext.Response.StatusCode = (int)code;

            return httpContext.Response.WriteAsync(result);
        }
    }
}
