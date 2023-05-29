using Consul;
using Chat.Api.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Chat.Api.Core.Infrastructure
{
    public static class ConsulStartup
    {
        private static IConfiguration _configuration;

        public static IServiceCollection AddConsulConfig(this IServiceCollection services, IConfiguration configuration)
        {

            _configuration = configuration;
            services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
            {
                var address = _configuration["ConsulHost"];//configuration.GetValue<string>("Consul:Host");
                consulConfig.Address = new Uri(address);
            }));
            return services;
        }

        public static IApplicationBuilder UseConsul(this IApplicationBuilder app)
        {
            var consulClient = app.ApplicationServices.GetRequiredService<IConsulClient>();
            var logger = app.ApplicationServices.GetRequiredService<ILoggerFactory>().CreateLogger("AppExtensions");
            var lifetime = app.ApplicationServices.GetRequiredService<IApplicationLifetime>();

            //if (!(app.Properties["server.Features"] is FeatureCollection features)) return app;

            //var addresses = features.Get<IServerAddressesFeature>();
            //var address = addresses.Addresses.Last();

            //Console.WriteLine($"address={address}");

            //var uri = new Uri(address);
            var uri = new Uri(_configuration["ConsulUri"]);
            var registration = new AgentServiceRegistration()
            {
                ID = $"{_configuration["ConsulRegisterName"]}-{uri.Port}",
                // servie name  
                Name = _configuration["ConsulRegisterName"],
                Address = $"{uri.Host}",
                Port = uri.Port,
                //Check = new AgentServiceCheck
                //{
                //    HTTP = $"{uri}{nameof(WeatherForecastController).Replace("Controller", "")}",
                //    Interval = TimeSpan.FromSeconds(30),
                //    Timeout = TimeSpan.FromMinutes(1),
                //    Status = HealthStatus.Passing
                //},
                //Tags = new[] { Environment.MachineName, Environment.UserName, Environment.UserDomainName, Dns.GetHostEntry(Dns.GetHostName()).AddressList.Aggregate("", (address1, address2) => $"{address1} - {address2}") }
            };

            logger.LogInformation("Registering with Consul");
            consulClient.Agent.ServiceDeregister(registration.ID).ConfigureAwait(true);
            consulClient.Agent.ServiceRegister(registration).ConfigureAwait(true);

            lifetime.ApplicationStopping.Register(() =>
            {
                logger.LogInformation("Unregistering from Consul");
                consulClient.Agent.ServiceDeregister(registration.ID).ConfigureAwait(true);
            });

            return app;
        }
    }
}
