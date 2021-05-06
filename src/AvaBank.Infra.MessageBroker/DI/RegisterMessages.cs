using Microsoft.Extensions.DependencyInjection;
using Liquid.Messaging.Extensions;

namespace AvaBank.Infra.MessageBroker
{
    public static class RegisterMessages
    {
        public static IServiceCollection RegisterMessage(this IServiceCollection services)
        {
            services.AddProducersConsumers(typeof(RegisterMessages).Assembly);

            return services;
        }
    }
}
