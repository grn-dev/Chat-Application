using EventBus.Abstractions;
using Chat.Domain.Attributes;
using Chat.Domain.Events;
using IntegrationEventLogEF;
using IntegrationEventLogEF.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.Infra.Data.Logging;

namespace Chat.Infra.Data.EventSourcing
{
    [Bean]
    public class PublishIntegrationEventService : IPublishIntegrationEventService
    {
        private readonly IIntegrationEventLogService _eventLogService;
        private readonly IntegrationEventLogContext _integrationEventLogContext;
        private readonly IEventBus _eventBus;
        public PublishIntegrationEventService(Func<DbConnection, IIntegrationEventLogService> integrationEventLogServiceFactory, IEventBus eventBus, IntegrationEventLogContext integrationEventLogContext)
        {
            _integrationEventLogContext = integrationEventLogContext;
            _eventLogService = integrationEventLogServiceFactory(_integrationEventLogContext.Database.GetDbConnection());
            _eventBus = eventBus;
            // _logCollection = logCollection;
        }

        public async Task PublishEventsThroughEventBusAsync(Guid transactionId)
        {
            var pendingLogEvents = await _eventLogService.RetrieveEventLogsPendingToPublishAsync(transactionId);

            foreach (var logEvt in pendingLogEvents)
            {
                //_logger.LogInformation("----- Publishing integration event: {IntegrationEventId} from {AppName} - ({@IntegrationEvent})", logEvt.EventId, Program.AppName, logEvt.IntegrationEvent);

                try
                {
                    await _eventLogService.MarkEventAsInProgressAsync(logEvt.EventId);
                    _eventBus.Publish(logEvt.IntegrationEvent);
                    await _eventLogService.MarkEventAsPublishedAsync(logEvt.EventId);
                }
                catch (Exception ex)
                {
                    //_logger.LogError(ex, "ERROR publishing integration event: {IntegrationEventId} from {AppName}", logEvt.EventId, Program.AppName);

                    await _eventLogService.MarkEventAsFailedAsync(logEvt.EventId);
                }
            }
        }
    }
}
