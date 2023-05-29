using NetDevPack.Messaging;
using System;

namespace Chat.Domain.Models.Events
{
    public class ChannelRegisteredEvent : Event
    {
        public ChannelRegisteredEvent(Guid aggregateId, Func<int> idAccessor)
        {
            IdAccessor = idAccessor;
            AggregateId = aggregateId;
        }
        public Func<int> IdAccessor { get; set; }
    }
}