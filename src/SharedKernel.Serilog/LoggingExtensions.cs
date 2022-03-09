using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace SharedKernel.Serilog
{
    public static class LoggingExtensions
    {
        public static ILoggingBuilder ConfigureLogging(this ILoggingBuilder builder, IConfiguration config)
        {
            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                .CreateLogger();

            return builder
                .ClearProviders()
                .AddSerilog(logger, dispose: true);
        }
    }
}