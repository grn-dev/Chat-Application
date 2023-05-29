using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using System.Net;
using System.Threading.Tasks;
using Chat.Hubs;

namespace Chat.Api.Core.Infrastructure
{
    public static class SignalRStartUp
    {
        public static IServiceCollection AddSignalRModule(this IServiceCollection @this)
        {

            @this.AddSignalR(hubOptions =>
            {
                hubOptions.EnableDetailedErrors = true;
                //hubOptions.KeepAliveInterval = TimeSpan.FromSeconds(5);
                //hubOptions.ClientTimeoutInterval= TimeSpan.FromSeconds(10);
                hubOptions.AddFilter<CustomFilter>();
            })
            //.AddHubOptions<ChatHub>(options =>
            //{
            //    // Local filters will run second
            //    options.AddFilter<CustomFilter>();
            //})
            .AddStackExchangeRedis("redis-master:6379", options =>
            {
                options.Configuration.ChannelPrefix = "MyApp";
            }).AddStackExchangeRedis(o =>
            {
                o.ConnectionFactory = async writer =>
                {
                    var config = new ConfigurationOptions
                    {
                        AbortOnConnectFail = false
                    };
                    config.EndPoints.Add(IPAddress.Loopback, 0);
                    config.SetDefaultPorts();
                    var connection = await ConnectionMultiplexer.ConnectAsync(config, writer);
                    connection.ConnectionFailed += (_, e) =>
                    {
                        Console.WriteLine("Connection to Redis failed.");
                    };

                    if (!connection.IsConnected)
                    {
                        Console.WriteLine("Did not connect to Redis.");
                    }

                    return connection;
                };
            });
            @this.AddSingleton<IUserIdProvider, UserIdProvider>();
            return @this;
        }

        public static IApplicationBuilder UseApplicationSignalR(this IApplicationBuilder @this)
        {
            @this.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/ChatHub");
            });

            return @this;
        }
        public class CustomFilter : IHubFilter
        {
            public async ValueTask<object> InvokeMethodAsync(
                HubInvocationContext invocationContext, Func<HubInvocationContext, ValueTask<object>> next)
            {
                Console.WriteLine($"Calling hub method '{invocationContext.HubMethodName}'");
                try
                {
                    return await next(invocationContext);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception calling '{invocationContext.HubMethodName}': {ex}");
                    throw new HubException(ex.Message);
                    throw;
                }
            } 
            // Optional method
            public Task OnConnectedAsync(HubLifetimeContext context, Func<HubLifetimeContext, Task> next)
            {
                return next(context);
            } 
            // Optional method
            public Task OnDisconnectedAsync(
                HubLifetimeContext context, Exception exception, Func<HubLifetimeContext, Exception, Task> next)
            {
                return next(context, exception);
            }
        }
    }
}
