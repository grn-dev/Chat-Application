using Autofac;
using Autofac.Extensions.DependencyInjection;
using Chat.Api.Core.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Chat.Infra.Ioc;

namespace Chat.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            CurrentEnvironment = webHostEnvironment;
        }

        public IConfiguration Configuration { get; }
        private IWebHostEnvironment CurrentEnvironment { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            services.AddWebModule()
                 .AddAutoMapperModule()
                 .AddDatabaseModule(Configuration)
                 .AddProblemDetailsModule()
                 .AddConsulConfig(Configuration)
                 .AddSecurityModule(CurrentEnvironment, Configuration)
                 .AddEventBus(Configuration)
                 .AddSwaggerModule(Configuration)
                 .AddMediatR(Assembly.GetExecutingAssembly()).AddDependencyInjectionModule()
                 .AddClientConfigurations(Configuration)
                 .AddApiVersioningMaka()
                 .AddSignalRModule()
                 .AddLang()
                 .AddCacheService(Configuration);



            var container = new ContainerBuilder();
            container.Populate(services);

            new AutofacServiceProvider(container.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {


            app.UseApplicationWeb()
               .UseApplicationProblemDetails()
               .UseConsul()
               .UseApplicationSecurity()
               .UseApplicationSignalR()
               .UseApplicationSwagger()
               .ConfigureEventBus()
               .UseApplicationLang();


            app.UseWebSockets();
            //app.UseOcelot().Wait();

        }
    }
}
