using NetDevPack.Messaging;
using System;

namespace Chat.Domain.Models.Events
{
    public class ChatRoomMessageRegisteredEvent : Event
    {
        public ChatRoomMessageRegisteredEvent(Guid aggregateId, Func<int> chatRoomMessageId, int chatRoomId)
        {
            ChatRoomId = chatRoomId;
            ChatRoomMessageId = chatRoomMessageId;
            AggregateId = aggregateId;
        }

        public Func<int> ChatRoomMessageId { get; set; }
        public int ChatRoomId { get; set; }
    }
}