namespace IdentityService
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
            var assembly = typeof(Program).Assembly.GetName().Name;
            var connectionString = builder.Configuration.GetConnectionString("IdentityDbConnection");

            //builder.Services.AddControllers();
            builder.Services.AddRazorPages();

            // Add ASP.NET Core Identity services
            builder.Services
                .AddDbContext<IdentityDataContext>(options =>
                    options.UseSqlServer(connectionString, sql =>
                        sql.MigrationsAssembly(assembly)))
                .AddIdentity<IdentityUser<int>, IdentityRole<int>>()
                .AddEntityFrameworkStores<IdentityDataContext>()
                .AddDefaultTokenProviders();

            // Add Duende IdentityServer services
            builder.Services
                .AddIdentityServer()
                .AddAspNetIdentity<IdentityUser<int>>()
                .AddConfigurationStore(options =>
                {
                    options.DefaultSchema = "Configuration";
                    options.ConfigureDbContext = (context) =>
                        context.UseSqlServer(connectionString, sql =>
                            sql.MigrationsAssembly(assembly));
                })
                .AddOperationalStore(options =>
                {
                    options.DefaultSchema = "Operational";
                    options.ConfigureDbContext = (context) =>
                        context.UseSqlServer(connectionString, sql =>
                            sql.MigrationsAssembly(assembly));
                })
                .AddDeveloperSigningCredential();

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

            app.UseStaticFiles();

            app.UseRouting();

            app.UseIdentityServer();
            app.UseAuthorization();

            //app.MapControllers().RequireAuthorization();
            app.MapRazorPages().RequireAuthorization();

            return app;
        }
    }
}