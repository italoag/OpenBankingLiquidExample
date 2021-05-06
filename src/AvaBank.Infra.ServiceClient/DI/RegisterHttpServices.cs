using Microsoft.Extensions.DependencyInjection;
using Liquid.Services.Http;

namespace AvaBank.Infra.ServiceClient.DI
{
    public static class RegisterHttpServices
    {
        public static IServiceCollection RegisterHttpService(this IServiceCollection services)
        {
            services.AddHttpServices(typeof(RegisterHttpServices).Assembly);

            return services;
        }
    }
}
