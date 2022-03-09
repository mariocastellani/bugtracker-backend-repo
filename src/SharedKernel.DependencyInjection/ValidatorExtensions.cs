using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace SharedKernel.DependencyInjection
{
    public static class ValidatorExtensions
    {
        public static IServiceCollection AddValidators(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            var assemblies = AssemblyHelper
                .GetApplicationAssemblies()
                .ToArray();

            foreach(var assembly in assemblies)
                services.AddValidatorsFromAssembly(assembly, lifetime);

            return services;
        }
    }
}