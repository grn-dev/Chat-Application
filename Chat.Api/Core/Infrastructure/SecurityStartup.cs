using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Chat.Api.Core.Infrastructure
{
    public static class SecurityStartup
    {
        public static IServiceCollection AddSecurityModule(this IServiceCollection @this, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            @this.AddCors(options => options.AddPolicy("CorsPolicy",
            builder =>
            {
                builder.AllowAnyHeader()
                       .AllowAnyMethod()
                       .SetIsOriginAllowed((host) => true)
                       .AllowCredentials();
            }));

            @this.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
             .AddJwtBearer(options =>
             {
                 options.Authority = configuration["AuthorityUrl"];
                 options.RequireHttpsMetadata = false;
                 options.Audience = configuration["Audience"];
                 options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                 {
                     NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",
                     RoleClaimType = "rl",
                     ValidateIssuer = false,
                     ValidateLifetime = true,
                     ClockSkew = TimeSpan.Zero
                 };
                 options.Events = new JwtBearerEvents
                 {
                     OnMessageReceived = context =>
                     {
                         var accessToken = context.Request.Query["token"];

                         // If the request is for our hub...
                         var path = context.HttpContext.Request.Path;
                         if (!string.IsNullOrEmpty(accessToken) &&
                             (path.StartsWithSegments("/ChatHub")))
                         {
                             // Read the token out of the query string
                             context.Token = accessToken;
                         }
                         return Task.CompletedTask;
                     }
                 };
             });
            return @this;


        }

        public static IApplicationBuilder UseApplicationSecurity(this IApplicationBuilder @this)
        {
            @this.UseHsts();
            @this.UseHttpsRedirection();
            @this.UseAuthentication();
            @this.UseAuthorization();
            @this.UseCors("CorsPolicy");


            return @this;
        }


    }
}
