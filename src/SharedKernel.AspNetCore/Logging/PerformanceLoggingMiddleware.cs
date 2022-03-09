using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace SharedKernel.AspNetCore.Logging
{
    public class PerformanceLoggingMiddleware
    {
        private const string MESSAGE_TEMPLATE = "{RequestMethod}: {RequestPath} [{StatusCode}] ({Elapsed:0.0000}ms)";
        private const string ERROR_TEMPLATE = "{RequestMethod}: {RequestPath} ({Elapsed:0.0000}ms)";

        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public PerformanceLoggingMiddleware(RequestDelegate next, ILogger<PerformanceLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        private static double GetElapsedMilliseconds(long start, long stop)
        {
            return (stop - start) * 1000 / (double)Stopwatch.Frequency;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var start = Stopwatch.GetTimestamp();
            try
            {
                await _next(context);

                var elapsed = GetElapsedMilliseconds(start, Stopwatch.GetTimestamp());
                var statusCode = context.Response?.StatusCode;
                
                _logger.LogDebug(MESSAGE_TEMPLATE, context.Request.Method, context.Request.Path, statusCode, elapsed);
            }
            catch
            {
                var elapsed = GetElapsedMilliseconds(start, Stopwatch.GetTimestamp());
                _logger.LogError(ERROR_TEMPLATE, context.Request.Method, context.Request.Path, elapsed);

                throw;
            }
        }
    }
}