using EventBus.Abstractions;
using EventBus.Events;
using Chat.Domain.Events;
using Chat.Infra.Data.Context;
using IntegrationEventLogEF;
using IntegrationEventLogEF.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Infra.Data.EventSourcing
{


    public class IntegrationEventService : IIntegrationEventService
    {
        private readonly Func<DbConnection, IIntegrationEventLogService> _integrationEventLogServiceFactory;
        private readonly IEventBus _eventBus;
        private readonly ChatContext _ChatContext;
        private readonly IIntegrationEventLogService _eventLogService;
        private readonly ILogger<IntegrationEventService> _logger;

        public IntegrationEventService(IEventBus eventBus,
            ChatContext MakaCommunicateContext,
            Func<DbConnection, IIntegrationEventLogService> integrationEventLogServiceFactory,
            ILogger<IntegrationEventService> logger)
        {
            _ChatContext = MakaCommunicateContext ?? throw new ArgumentNullException(nameof(MakaCommunicateContext));
            _integrationEventLogServiceFactory = integrationEventLogServiceFactory ?? throw new ArgumentNullException(nameof(integrationEventLogServiceFactory));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _eventLogService = _integrationEventLogServiceFactory(MakaCommunicateContext.Database.GetDbConnection());
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task AddAndSaveEventAsync(IntegrationEvent evt)
        {
            _logger.LogInformation("----- Enqueuing integration event {IntegrationEventId} to repository ({@IntegrationEvent})", evt.Id, evt);

            await _eventLogService.SaveEventAsync(evt, _ChatContext.GetCurrentTransaction());
        }
    }
}
