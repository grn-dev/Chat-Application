using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Garnet.Detail.Pagination.Asp;
using Garnet.Detail.Pagination.Asp.DependencyInjection;
using Garnet.Detail.Pagination.ListExtensions.DependencyInjection;
using Garnet.Pagination.Configurations;
using Newtonsoft.Json.Serialization;
using Garnet.Detail.Pagination.ListExtensions.Infrastructure;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Chat.Api.Core.Infrastructure
{
    public class SnakeUpperCaseNaming : SnakeCaseNamingStrategy
    {
        protected override string ResolvePropertyName(string name)
        {
            return base.ResolvePropertyName(name).ToUpper();
        }
    }
    public class EfAsyncIQueryable : IIQueryableAsyncMethods
    {
        public Task<List<TElement>> ToListAsync<TElement>(IQueryable<TElement> queryable)
        {
            return queryable.ToListAsync();
        }

        public Task<long> LongCountAsync<TElement>(IQueryable<TElement> queryable)
        {
            return queryable.LongCountAsync();
        }
    }
    public static class WebConfiguration
    {

        public static IServiceCollection AddWebModule(this IServiceCollection @this)
        {
            @this.AddHealthChecks();
            @this.AddControllers(options =>
            {

                options.ModelBinderProviders.Insert(0, new GarnetPaginationModelBinderProvider());
                //  AuditStartUp.AddAudit(options);
            })
                .AddNewtonsoftJson(op =>
                {
                    op.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    op.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter(new SnakeUpperCaseNaming()));
                });
            @this.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });

            @this.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            @this.AddGarnetPaginationAsp(new PaginationConfig() { StartPageNumber = StartPageNumber.Zero });

            return @this;
        }
        public static IApplicationBuilder UseApplicationWeb(this IApplicationBuilder @this)
        {
            @this.UseDefaultFiles();
            @this.UseStaticFiles();

            @this.UseRouting();
            //@this.UseRouting();
            //@this.UseAuthentication();
            //@this.UseAuthorization();
            //@this.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});

            @this.UseGarnetPaginationListExtensions(new EfAsyncIQueryable());

            return @this;
        }

    }
}
