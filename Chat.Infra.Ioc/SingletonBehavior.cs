
using Chat.Domain.Attributes;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.Infra.Ioc
{
    [Scope(Domain.Enums.ServiceLifetime.Singleton)]
    internal class SingletonBehavior : IServiceBehavior
    {
        private IServiceCollection _serviceDescriptors;
        public SingletonBehavior(IServiceCollection serviceDescriptors)
        {
            _serviceDescriptors = serviceDescriptors;
        }


        IServiceCollection IServiceBehavior.Add(Type serviceType, Type implementationType)
        {
            return _serviceDescriptors.AddSingleton(serviceType, implementationType);
        }

        IServiceCollection IServiceBehavior.Add(Type serviceType)
        {
            return _serviceDescriptors.AddSingleton(serviceType);
        }
    }
}
