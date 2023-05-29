using Audit.WebApi;
using Chat.Application.Interfaces;
using Chat.Infra.Data.Context;
using Chat.Infra.Data.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Chat.Domain.Core.SeedWork.Pagination;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Chat.Api.Controllers;
using Audit.Core; 
namespace Chat.Api.Core.Infrastructure
{
    //public class MyPropertyIgnoreResolver : DefaultContractResolver
    //{
    //    protected override List<MemberInfo> GetSerializableMembers(Type objectType)
    //    {
    //        var members = base.GetSerializableMembers(objectType);

    //        if (objectType != typeof(Pageable) && objectType != typeof(IPageable))
    //            return members;

    //        var ignoredProp = new List<string>()
    //        {
    //            nameof(IPageable.First),
    //            nameof(IPageable.Next),
    //            nameof(IPageable.PreviousOrFirst),
    //            nameof(Pageable.Previous),
    //        };


    //        return members.Where(info => !ignoredProp.Contains(info.Name)).ToList();
    //    }
    //}

    public static class AuditStartUp
    {
        public static void AddAudit(MvcOptions mvcOptions)
        {
            mvcOptions.AddAuditFilter(config => config
               //.LogActionIf((c) =>
               //{
               //     c.get

               //    return true;
               //})
               .LogActionIf(descriptor =>
                   descriptor.ControllerName != nameof(WeatherForecastController).Replace("Controller", ""))
                // .LogRequestIf(c=>c.Method!="GET")
                //.LogAllActions()
                .WithEventType(LogCollection.RequestEventLogName)
                .IncludeHeaders()
                .IncludeRequestBody()
                .IncludeResponseHeaders()
                .IncludeResponseBody()
            );

            Audit.Core.Configuration.JsonSettings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                //ContractResolver = new MyPropertyIgnoreResolver()
            };
        }

        public static void AuditConfig(IServiceProvider services)
        {
            // Audit.EntityFramework.Configuration.Setup()
            //     .ForContext<MakaCommunicateContext>(config => config
            //         .IncludeEntityObjects()
            //         .AuditEventType(LogCollection.ContextEventLogName))
            //     .UseOptOut()
            //     .IgnoreAny(t => t.Name.EndsWith("History"));

            var httpContextAccessor = services.GetRequiredService<IHttpContextAccessor>();

            ICurrentUserService UserServiceAccessor() =>
                httpContextAccessor?.HttpContext?.RequestServices.GetRequiredService<ICurrentUserService>();

            ChatContext ChatContextAccessor() =>
                httpContextAccessor?.HttpContext?.RequestServices.GetRequiredService<ChatContext>();

            LogCollection LogCollectionAccessor() =>
                httpContextAccessor?.HttpContext?.RequestServices.GetRequiredService<LogCollection>();

            Audit.Core.Configuration.Setup().UseCustomProvider
            (
                new LogDataProvider(LogCollectionAccessor)
            );


            Audit.Core.Configuration.AddOnCreatedAction(scope =>
            {
                try
                {
                    var currentUserService = UserServiceAccessor();
                    // var MakaCommunicateContext = MakaCommunicateContextAccessor();

                    scope.SetCustomField("User",
                        new { Name = currentUserService?.UserName, Id = currentUserService?.UserId });

                    //scope.SetCustomField("TransactionId", MakaCommunicateContext.GetCurrentTransaction()?.TransactionId);

                    scope.SetCustomField("Microservice_Name", "Chat");
                }
                catch
                {

                }
            });

            Audit.Core.Configuration.AddCustomAction(ActionType.OnEventSaving, scope =>
            {
                try
                {
                    var ChatContext = ChatContextAccessor();
                    scope.SetCustomField("TransactionId",  ChatContext.GetCurrentTransactionId());
                }
                catch
                {

                }
                //var currentUserService = serviceProvider.GetRequiredService<ICurrentUserService>();

                //var auditAction = scope.Event.GetWebApiAuditAction();

                //if (auditAction == null)
                //{
                //    return;
                //}

                //// Removing sensitive headers
                //auditAction.Headers.Remove("Authorization");

                // Adding custom details to the log
                //    scope.Event.CustomFields.Add("User", new { Name = currentUserService.UserName, Id = currentUserService.UserId });

                //// Removing request body conditionally as an example
                //if (auditAction.HttpMethod.Equals("DELETE"))
                //{
                //    auditAction.RequestBody = null;
                //}
            });

            Configuration.AddCustomAction(ActionType.OnEventSaving,
                scope =>
                {
                    if (scope?.GetWebApiAuditAction()?.ResponseStatusCode >= 500)
                        return;

                    scope?.Discard();
                });
        }
    }
}