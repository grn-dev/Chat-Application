using Hellang.Middleware.ProblemDetails;

using Chat.Application.Configuration.Exceptions;
using Chat.Domain.Core.SeedWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using System.Threading.Tasks;
using Chat.Application.Configuration.Data;
using Grpc.Core;

namespace Chat.Api.Core.Infrastructure
{
    public static class ProblemDetailsStartup
    {


        public static IServiceCollection AddProblemDetailsModule(this IServiceCollection @this)
        {
            @this.ConfigureOptions<ProblemDetailsConfiguration>();
            @this.AddProblemDetails();

            return @this;
        }

        public static IApplicationBuilder UseApplicationProblemDetails(this IApplicationBuilder @this)
        {
            @this.UseProblemDetails();
            return @this;
        }
    }
    public class ProblemDetailsConfiguration : IConfigureOptions<ProblemDetailsOptions>
    {
        [Obsolete]
        public ProblemDetailsConfiguration(IHostingEnvironment environment, IHttpContextAccessor httpContextAccessor)
        {
            _environment = environment;
            _HttpContextAccessor = httpContextAccessor;
        }

        [Obsolete] private IHostingEnvironment _environment { get; }
        private IHttpContextAccessor _HttpContextAccessor { get; }

        [Obsolete]
        public void Configure(ProblemDetailsOptions options)
        {
            options.Map<BusinessRuleValidationException>
            (ex => new ProblemDetails()
            {
                Title = "",
                Detail = ex.Details,
                Status = (int)HttpStatusCode.BadRequest,
                Extensions =
                {
                    ["errorcode"] = $"{ex.ErroCode}"
                }
            });
            options.Map<RpcException>
            (ex =>
            {
                string errorcode = ex.Trailers.Where(c => c.Key == "errorcode").First().Value;
                return new ProblemDetails()
                {
                    Title = "",
                    Detail = ex.Trailers.Where(c => c.Key == "error").First().Value,
                    Status = (int)HttpStatusCode.BadRequest,
                    Extensions =
                    {
                        ["errorcode"] = errorcode//grpc Error
                    }
                };
            });

            options.Map<MyApplicationException>
            (ex => new ProblemDetails()
            {
                Title = ex.Name,
                Detail = ex.Details,
                Status = (int)HttpStatusCode.BadRequest,
                Extensions =
                {
                    ["errorcode"] = $"{ex.ErroCode}"
                }
            });
            options.Map<EntityNotFoundException>
            (ex => new ProblemDetails()
            {
                Title = "",
                Detail = ex.Details,
                Status = (int)HttpStatusCode.BadRequest,
                Extensions =
                {
                    ["message"] = $"{ex.ErroCode}",
                    ["entity"] = $"{ex.Entity}"
                }
            });
            options.Map<Exception>(ex => new ProblemDetails
            {
                Title = "Internal Server Error",
                Detail = "Internal Server Error",
                Status = (int)HttpStatusCode.InternalServerError
            });

            options.IncludeExceptionDetails = (ctx, ex) => { return false; };
            options.OnBeforeWriteDetails = (details, http) =>
            {
                // keep consistent with asp.net core 2.2 conventions that adds a tracing value
                var traceId = Activity.Current?.Id ?? _HttpContextAccessor.HttpContext.TraceIdentifier;
                http.Extensions["traceId"] = traceId;
            };

            options.Map<AuthenticationException>(exception =>
                new ExceptionProblemDetails(exception, StatusCodes.Status401Unauthorized));
            options.Map<NotImplementedException>(exception =>
                new ExceptionProblemDetails(exception, StatusCodes.Status501NotImplemented));

            //TODO add Headers to HTTP responses
        }
    }
}
