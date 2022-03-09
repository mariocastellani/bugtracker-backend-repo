using Microsoft.EntityFrameworkCore;

namespace ApiService.Data
{
    internal static class SeedData
    {
        public static void EnsureSeedData(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();

            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDataContext>();
                context.Database.Migrate();
            }
        }
    }
}