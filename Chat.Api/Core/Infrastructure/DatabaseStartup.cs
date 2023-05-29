using Chat.Application.Configuration.Data;
using Chat.Infra.Data.Context;
using IntegrationEventLogEF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Chat.Api.Core.Infrastructure
{
    public static class DatabaseConfiguration
    {
        public static IServiceCollection AddDatabaseModule(this IServiceCollection @this,
            IConfiguration configuration)
        {


            @this.AddDbContext<ChatContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            @this.AddDbContext<EventStoreSqlContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            @this.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>(s => new SqlConnectionFactory(configuration.GetConnectionString("DefaultConnection")));

            @this.AddDbContext<IntegrationEventLogContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                                     sqlServerOptionsAction: sqlOptions =>
                                     {
                                         sqlOptions.MigrationsAssembly(typeof(ChatContext).GetTypeInfo().Assembly.GetName().Name);
                                         //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
                                         sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                                     });
            });
            return @this;
        }

    }
}
