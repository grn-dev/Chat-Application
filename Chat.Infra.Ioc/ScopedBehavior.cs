using Chat.Domain.Attributes;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Chat.Infra.Ioc
{
    [Scope(Chat.Domain.Enums.ServiceLifetime.Scoped)]
    internal class ScopedBehavior : IServiceBehavior
    {
        private IServiceCollection _services;
        public ScopedBehavior(IServiceCollection services)
        {
            _services = services;
        }

        IServiceCollection IServiceBehavior.Add(Type serviceType, Type implementationType)
        {
            return _services.AddScoped(serviceType, implementationType);

        }

        IServiceCollection IServiceBehavior.Add(Type serviceType)
        {
            return _services.AddScoped(serviceType);
        }
    }
}
