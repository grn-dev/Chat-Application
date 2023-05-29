using NetDevPack.Messaging;
using System;

namespace Chat.Domain.Models.Events
{
    public class ChatRoomRegisteredEvent : Event
    {
        public ChatRoomRegisteredEvent(Guid aggregateId, Func<int> idAccessor, ChatRoomType type)
        {
            AggregateId = aggregateId;
            IdAccessor = idAccessor;
            Type = type;
        }

        public Func<int> IdAccessor { get; set; }
        public ChatRoomType Type { get; set; }
    }
}