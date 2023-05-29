using Chat.Domain.Commands.Behaviors;
using Chat.Infra.Data.Context;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Chat.Application.Configuration.Data;
using RestSharp;
using Chat.Infra.Data.Logging;
using Chat.Language;
using Chat.Language.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;

namespace Chat.Infra.Ioc
{
    public static class IocExtensions
    {
        public static IServiceCollection AddDependencies(this IServiceCollection @services)
        {
            new ServiceCollection(@services).AddServiceDescriptors();


            services.AddTransient<IRestClient, RestClient>();
            services.AddScoped<ChatContext>();
            services.AddScoped<EventStoreSqlContext>();
            services.AddScoped<LogCollection>();
            //services.AddSingleton<FirebaseMessaging>();

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionBehaviour<,>));
            return @services;
        }

        public static IServiceCollection AddLang(this IServiceCollection @services)
        {
            services.AddSingleton<EFStringLocalizerFactory>();
            services.AddSingleton<ILangUtility, LangUtility>();


            return @services;
        }

        public static IApplicationBuilder UseApplicationLang(this IApplicationBuilder @this)
        {
            var supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("en-US"),
                new CultureInfo("fa-IR")
            };

            var requestLocalizationOptions = new RequestLocalizationOptions
            {
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures,
            };
            requestLocalizationOptions.RequestCultureProviders.Insert(0, new JsonRequestCultureProvider());
            @this.UseRequestLocalization(requestLocalizationOptions);
            return @this;
        }

        public class JsonRequestCultureProvider : RequestCultureProvider
        {
            public override Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
            {
                if (httpContext == null)
                {
                    throw new ArgumentNullException(nameof(httpContext));
                }

                string culture = httpContext.Request.Headers["lang"];
                if (culture == null)
                    culture = "fa-IR";

                return Task.FromResult(new ProviderCultureResult("en-US", culture));
            }
        }
    }
}
