using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat.Domain.Core.SeedWork.Pagination;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Garnet.Standard.Pagination;

namespace Chat.Api.Core.Infrastructure
{
    public static class SwaggerStartUp
    {
        public static IServiceCollection AddSwaggerModule(this IServiceCollection @this, IConfiguration configuration)
        {
            @this.AddSwaggerGen(options =>
            {
                options.OperationFilter<IPageableOperationFilter>();

                //options.DescribeAllEnumsAsStrings();
                //options.SwaggerDoc("v1", new OpenApiInfo
                //{
                //    Title = "Acquirer -  HTTP API",
                //    Version = "v1",
                //    Description = "The Acquirer Service HTTP API"
                //});
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        { jwtSecurityScheme, Array.Empty<string>() }
                    });



            });
            @this.AddSwaggerGenNewtonsoftSupport();
            return @this;

        }
        public static IApplicationBuilder UseApplicationSwagger(this IApplicationBuilder @this)
        {
            @this.UseSwagger()
             .UseSwaggerUI(c =>
             {
                 c.OAuthClientId("ApigateWay");
                 c.OAuthClientSecret("asd123");
                 c.OAuthAppName("Acquirer Swagger UI");
                 //c.SwaggerEndpoint($"/swagger/v3/swagger.json", $"v3");
                 //c.SwaggerEndpoint($"/swagger/v2/swagger.json", $"v2");
                 c.SwaggerEndpoint($"/swagger/v1/swagger.json", $"v1");
             });

            return @this;
        }
    }
    internal class IPageableOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (context.ApiDescription.ParameterDescriptions.All(description =>
                description.ParameterDescriptor.ParameterType != typeof(IPagination)))
                return;

            var namesToRemove = context.ApiDescription.ParameterDescriptions
                .Where(description => description.ParameterDescriptor.ParameterType == typeof(IPagination)).ToList()
                .Select(description => description.Name)
                .Where(name => !new List<string>
                {
                    nameof(IPagination.PageNumber),
                    nameof(IPagination.PageSize),
                    nameof(IPagination.Filters),
                    nameof(IPagination.Orders)
                }.Contains(name));

            operation.Parameters = operation.Parameters.Where(parameter => !namesToRemove.Contains(parameter.Name))
                .ToList();

            var dic = new Dictionary<string, string>
            {
                [nameof(IPagination.PageNumber)] = "page",
                [nameof(IPagination.PageSize)] = "size",
                [nameof(IPagination.Filters)] = "filter",
                [nameof(IPagination.Orders)] = "order",
            };

            foreach (var openApiParameter in operation.Parameters)
                if (dic.ContainsKey(openApiParameter.Name))
                    openApiParameter.Name = dic[openApiParameter.Name];
        }
    }
}
