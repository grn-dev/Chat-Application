using System;
using NetDevPack.Messaging;

namespace Chat.Domain.Models.Events
{
    public class DirectMessageRegisteredEvent : Event
    {
        public DirectMessageRegisteredEvent(Guid aggregateId, Func<int> directMessageId, int directId)
        {
            DirectMessageId = directMessageId;
            DirectId = directId;
            AggregateId = aggregateId;
        }

        public Func<int> DirectMessageId { get; set; }
        public int DirectId { get; set; }
    }
}