using Edu_System_BackEnd.Edu_System_BackEnd.Core.Exceptions;
using System.Net;
using System.Text.Json;

namespace Edu_System_BackEnd.Edu_System_BackEnd.API.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                var cancellationToken = context.RequestAborted;
                await _next(context);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("Opaeration canceled by user");
                context.Response.StatusCode = (int)HttpStatusCode.RequestTimeout;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            int statusCode = exception switch
            {
                BaseException baseEx => baseEx.StatusCode,
                ArgumentException => (int)HttpStatusCode.BadRequest,
                KeyNotFoundException => (int)HttpStatusCode.NotFound,
                UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                _ => (int)HttpStatusCode.InternalServerError
            };

            var result = JsonSerializer.Serialize(new 
            { 
                error = exception.Message,
                statusCode,
                details = exception is BaseException baseException ? baseException.ExceptionDetail : null
            });

            response.StatusCode = statusCode;
            return response.WriteAsync(result);
        }
    }
}
