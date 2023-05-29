using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Events
{
    public interface IPublishIntegrationEventService
    {
        Task PublishEventsThroughEventBusAsync(Guid transactionId);
    }
}
