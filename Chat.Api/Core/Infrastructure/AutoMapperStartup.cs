using Chat.Infra.Ioc.AutoMapperDI;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Api.Core.Infrastructure
{
    public static class AutoMapperStartup
    {
        public static IServiceCollection AddAutoMapperModule(this IServiceCollection @this)
        {

            @this.ApplyAllConfigurations();
            return @this;
        }
    }
}
