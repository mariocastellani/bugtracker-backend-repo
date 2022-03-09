using Microsoft.Extensions.DependencyInjection;

namespace SharedKernel.DependencyInjection
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            var irepoType = typeof(IReadonlyRepository<>);

            var repoTypes = AssemblyHelper.GetApplicationAssemblies()
                .SelectMany(assembly => assembly.GetTypes().Where(type =>
                {
                    return
                        type.IsClass && !type.IsAbstract &&
                        type.GetInterface(irepoType.Name) != null;
                }))
                .ToArray();

            
            foreach (var repoType in repoTypes)
            {
                // Get the most concrete repo interface
                var interfaceType = TypeHelper.GetMostConcreteInterfaceType(repoType, irepoType);
                if (interfaceType == null)
                    continue;

                // Register the repo as a service
                switch (lifetime)
                {
                    case ServiceLifetime.Singleton:
                        services.AddSingleton(interfaceType, repoType);
                        break;

                    case ServiceLifetime.Scoped:
                        services.AddScoped(interfaceType, repoType);
                        break;

                    case ServiceLifetime.Transient:
                        services.AddTransient(interfaceType, repoType);
                        break;
                }
            }

            return services;
        }
    }
}