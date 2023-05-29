using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.Infra.Ioc
{
    internal interface IServiceBehavior
    {


        IServiceCollection Add(Type serviceType, Type implementationType);

        IServiceCollection Add(Type serviceType);
    }
}
