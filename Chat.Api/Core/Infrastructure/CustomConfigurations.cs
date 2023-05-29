using Chat.Infra.Clients;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Chat.Api.Core.Infrastructure
{
    public static class CustomConfigurations
    {
        public static IServiceCollection AddClientConfigurations(this IServiceCollection serviceCollection,
            IConfiguration configuration)
        {
            serviceCollection.Configure<ClientConfig>(configuration.GetSection("Client"));

            serviceCollection.AddSingleton(provider => provider.GetRequiredService<IOptions<ClientConfig>>().Value);

            return serviceCollection;
        }
    }
}