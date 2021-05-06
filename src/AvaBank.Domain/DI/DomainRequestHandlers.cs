using Liquid.Domain.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace AvaBank.Domain.DI
{
    public static class DomainRequestHandlers
    {
        public static IServiceCollection RegisterDomainConfigs(this IServiceCollection services)
        {
            services.AddDomainRequestHandlers(typeof(DomainRequestHandlers).Assembly);

            return services;
        }
    }
}