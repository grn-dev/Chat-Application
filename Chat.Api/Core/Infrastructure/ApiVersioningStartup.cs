using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Chat.Api.Core.Infrastructure
{
    public static class ApiVersioningStartup
    {
        public static IServiceCollection AddApiVersioningMaka(this IServiceCollection @services)
        {
            #region Api Versioning
            // Add API Versioning to the Project
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.ReportApiVersions = true;
                config.AssumeDefaultVersionWhenUnspecified = true;
            });
            #endregion


            services.AddVersionedApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });
            services.AddSwaggerGen();
            services.ConfigureOptions<ConfigureSwaggerOptions>();
            //services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();


            return @services;

        }
        //public static IApplicationBuilder UseSwaggerVersioning(this IApplicationBuilder app)
        //{
        //    app.UseSwagger();
        //    app.UseSwaggerUI(c =>
        //    {
        //        c.SwaggerEndpoint($"/swagger/v3/swagger.json", $"v3");
        //        c.SwaggerEndpoint($"/swagger/v2/swagger.json", $"v2");
        //        c.SwaggerEndpoint($"/swagger/v1/swagger.json", $"v1");
        //    });
        //    return app;
        //}

        public class ConfigureSwaggerOptions
        : IConfigureNamedOptions<SwaggerGenOptions>
        {
            private readonly IApiVersionDescriptionProvider provider;

            public ConfigureSwaggerOptions(
                IApiVersionDescriptionProvider provider)
            {
                this.provider = provider;
            }

            public void Configure(SwaggerGenOptions options)
            {
                // add swagger document for every API version discovered
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(
                        description.GroupName,
                        CreateVersionInfo(description));
                }
            }

            public void Configure(string name, SwaggerGenOptions options)
            {
                Configure(options);
            }

            private OpenApiInfo CreateVersionInfo(
                    ApiVersionDescription description)
            {
                var info = new OpenApiInfo()
                {
                    Title = "Chat API",
                    Version = description.ApiVersion.ToString()
                };

                if (description.IsDeprecated)
                {
                    info.Description += " This API version has been deprecated.";
                }

                return info;
            }
        }

    }
}