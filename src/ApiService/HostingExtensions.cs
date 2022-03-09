namespace ApiService
{
    internal static class HostingExtensions
    {
        public static WebApplicationBuilder ConfigureBuilder(this WebApplicationBuilder builder)
        {
            Directory.SetCurrentDirectory(AppContext.BaseDirectory);

            // Configure logging providers
            builder.Logging.ConfigureLogging(builder.Configuration);

            return builder;
        }

        public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers(options => 
                options.Filters.Add<ResultToActionResultFilterAttribute>());

            builder.Services.AddSwagger(builder.Environment);

            // Configure JWT Authentication services
            builder.Services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.RequireHttpsMetadata = !builder.Environment.IsDevelopment();
                    options.Authority = "https://localhost:44380/auth";
                    options.Audience = "bugtracker-api";
                });

            // Configure Authorization services and policies
            builder.Services
                .AddAuthorization(options =>
                {
                    options.AddPolicy("readonly", policy =>
                    {
                        policy.RequireAuthenticatedUser();
                        policy.RequireClaim("scope", "bugtracker.read");
                    });

                    options.AddPolicy("admins", policy =>
                    {
                        policy.RequireAuthenticatedUser();
                        policy.RequireClaim("scope", "manage");
                        policy.RequireRole("Administrator");
                    });
                });

            var connectionString = builder.Configuration.GetConnectionString("BugTrackerDbConnection");
            builder.Services.AddSqlServerDataContext<ApplicationDataContext>(connectionString);
            builder.Services.AddInternalMessaging();
            builder.Services.AddRepositories();
            builder.Services.AddValidators();

            // Insert seed data if you pass "--seed true" parameter
            if (builder.Configuration.GetValue<bool>("seed"))
                builder.Services.EnsureSeedData();

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

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseSwagger(app.Environment);

            app.UseMiddleware<PerformanceLoggingMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers().RequireAuthorization();

            return app;
        }
    }
}