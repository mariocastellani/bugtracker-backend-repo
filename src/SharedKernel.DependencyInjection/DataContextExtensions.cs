using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.EntityFrameworkCore;

namespace SharedKernel.DependencyInjection
{
    public static class DataContextExtensions
    {
        public static IServiceCollection AddInMemoryDataContext<TContext>(this IServiceCollection services, ServiceLifetime contextLifetime = ServiceLifetime.Scoped, ServiceLifetime optionsLifetime = ServiceLifetime.Scoped)
            where TContext : BaseDbContext
        {
            return services
                .AddDbContext<TContext>(options => options
                    .UseInMemoryDatabase(typeof(TContext).FullName), contextLifetime, optionsLifetime);
        }

        public static IServiceCollection AddSqlServerDataContext<TContext>(this IServiceCollection services, string connectionString, ServiceLifetime contextLifetime = ServiceLifetime.Scoped, ServiceLifetime optionsLifetime = ServiceLifetime.Scoped)
            where TContext : BaseDbContext
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            return services
                .AddDbContext<TContext>(options => options
                    .UseSqlServer(connectionString, sql => sql
                        .MigrationsAssembly(typeof(TContext).Assembly.GetName().Name)),
                    contextLifetime, optionsLifetime);
        }
    }
}