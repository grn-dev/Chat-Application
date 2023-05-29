using Autofac;
using EventBus;
using EventBus.Abstractions;
using EventBus.Events;
using EventBusRabbitMQ;
using Chat.Domain.Events;
using Chat.Infra.Data.EventSourcing;
using IntegrationEventLogEF.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;

namespace Chat.Api.Core.Infrastructure
{
    public static class EventBusStartUp
    {
        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IEventBus, EventBusRabbitMQ.EventBusRabbitMQ>(sp =>
             {
                 var subscriptionClientName = configuration["SubscriptionClientName"];
                 var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                 var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                 var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ.EventBusRabbitMQ>>();
                 var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                 var retryCount = 5;
                 if (!string.IsNullOrEmpty(configuration["EventBusRetryCount"]))
                 {
                     retryCount = int.Parse(configuration["EventBusRetryCount"]);
                 }

                 return new EventBusRabbitMQ.EventBusRabbitMQ(rabbitMQPersistentConnection, logger, iLifetimeScope, eventBusSubcriptionsManager, subscriptionClientName, retryCount);
             });
            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
            services.AddScoped<IIntegrationEventService, IntegrationEventService>();
            //services.AddTransient<UserRegisteredIntegrationEventHandler>();


            var types = Assembly.Load("Chat.Application")
                  .GetTypes()
                  .Where(t => t.Name.EndsWith(nameof(IntegrationEvent)))
                  .ToList();

            services.AddTransient<Func<DbConnection, IIntegrationEventLogService>>(
             sp => (DbConnection c) => new IntegrationEventLogService(c, types));

            //services.AddTransient<IIntegrationEventLogService>(
            //         sp =>
            //         {
            //             var x = sp.GetRequiredService<IdentityServerContext>();

            //             return new IntegrationEventLogService(x.Database.GetDbConnection(), types);
            //         });

            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();

                //var test= ConnectionFactoryBuilder.BuildConnectionFactory
                // (configuration["EventBusConnection"], true, configuration["EventBusUserName"], configuration["EventBusPassword"]);
                var factory = new ConnectionFactory()
                {
                    HostName = configuration["EventBusConnection"],
                    DispatchConsumersAsync = true
                };

                if (!string.IsNullOrEmpty(configuration["EventBusUserName"]))
                {
                    factory.UserName = configuration["EventBusUserName"];
                }

                if (!string.IsNullOrEmpty(configuration["EventBusPassword"]))
                {
                    factory.Password = configuration["EventBusPassword"];
                }

                var retryCount = 5;
                if (!string.IsNullOrEmpty(configuration["EventBusRetryCount"]))
                {
                    retryCount = int.Parse(configuration["EventBusRetryCount"]);
                }

                return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
            });

            return services;
        }
        public static IApplicationBuilder ConfigureEventBus(this IApplicationBuilder @this)
        {
            var eventBus = @this.ApplicationServices.GetRequiredService<IEventBus>();

            //eventBus.Subscribe<UserRegisteredIntegrationEvent, UserRegisteredIntegrationEventHandler>();


            return @this;
        }
    }
}
