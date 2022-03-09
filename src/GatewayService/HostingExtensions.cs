namespace GatewayService
{
    internal static class HostingExtensions
    {
        public static WebApplicationBuilder ConfigureBuilder(this WebApplicationBuilder builder)
        {
            Directory.SetCurrentDirectory(AppContext.BaseDirectory);

            // Add Ocelot config files
            builder.Configuration
                .AddJsonFile("ocelot.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

            // Configure logging providers
            builder.Logging.ConfigureLogging(builder.Configuration);

            return builder;
        }

        public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
        {
            // Add Ocelot services
            builder.Services.AddOcelot(builder.Configuration);

            return builder;
        }

        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            // Configure the HTTP request pipeline

            if (!app.Environment.IsDevelopment())
            {
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            app.UseMiddleware<PerformanceLoggingMiddleware>();

            app.UseOcelot().Wait();

            return app;
        }
    }
}