using Chat.Infra.Ioc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Api.Core.Infrastructure
{
    public static class DependencyInjectionStartup
    {
        public static IServiceCollection AddDependencyInjectionModule(this IServiceCollection @this)
        {
            @this.AddDependencies();
            return @this;
        }
    }
}
