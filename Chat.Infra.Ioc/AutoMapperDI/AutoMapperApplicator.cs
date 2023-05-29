using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace Chat.Infra.Ioc.AutoMapperDI
{
    public static class AutoMapperApplicator
    {
        public static IServiceCollection ApplyAllConfigurations(this IServiceCollection @this, Action<IMapperConfigurationExpression> customerProfileRegister = null)
        {
            var profiles = Assembly.Load("Chat.Application")
              .GetTypes()
              .Where(t => typeof(Profile).GetTypeInfo().IsAssignableFrom(t.GetTypeInfo()))
              .Where(t => !t.GetTypeInfo().IsAbstract)
              .ToList();


            var mappingConfig = new MapperConfiguration(mc =>
            {
                profiles.ForEach(c => mc.AddProfile(c));

                customerProfileRegister?.Invoke(mc);
            });

            IMapper mapper = mappingConfig.CreateMapper();
            @this.AddSingleton(mapper);

            Data.Domain.Pagination.QueryableExtensions._mapperConfiguration = mappingConfig;
            return @this;
        }
    }
}
