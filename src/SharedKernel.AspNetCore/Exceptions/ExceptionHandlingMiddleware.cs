using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace SharedKernel.AspNetCore.Exceptions
{
    /// <summary>
    /// ASP.NET Core middleware for exception handling.
    /// This middleware logs unhandled exceptions and then returns them in a human-readable form. 
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var traceId = Guid.NewGuid().ToString("N");

            // Logs the exception whether it is not a ValidationException
            if (exception is not ValidationException)
                _logger.LogError(exception, "Exception Occurred. TraceId: {TraceId}", traceId);

            // Exception to return in the Response
            var resultException = exception is not ValidationException
                ? new Exception("We are sorry! Something went wrong.")
                : exception;

            // Sets Response properties
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = exception is ValidationException
                ? StatusCodes.Status422UnprocessableEntity
                : StatusCodes.Status500InternalServerError;

            // Writes ErrorDetails to the Response body
            await context.Response.WriteAsync(new ErrorDetails
            {
                StatusCode = context.Response.StatusCode,
                Message = resultException.Message,
                TraceId = traceId
            }.ToString());
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
    }
}