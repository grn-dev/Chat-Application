using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Microsoft.Extensions.Configuration;

namespace Chat.Api.Core.Infrastructure
{
    public static class CacheServiceStartup
    {
        public static IServiceCollection AddCacheService(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString =
                $"{configuration.GetValue<string>("Redis:Server")}:{configuration.GetValue<int>("Redis:Port")}";

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration =
                    connectionString;
            });

            services.AddSingleton<IConnectionMultiplexer>(
                ConnectionMultiplexer.Connect(connectionString)
            );

            return services;
        }
    }
}