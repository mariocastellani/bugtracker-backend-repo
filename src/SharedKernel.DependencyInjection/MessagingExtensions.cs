using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace SharedKernel.DependencyInjection
{
    public static class MessagingExtensions
    {
        public static IServiceCollection AddInternalMessaging(this IServiceCollection services)
        {
            var assemblies = AssemblyHelper
                .GetApplicationAssemblies()
                .ToArray();

            return services.AddMediatR(assemblies);
        }
    }
}