using NetDevPack.Messaging;
using System;

namespace Chat.Domain.Models.Events
{
    public class ChatRoomUserRegisteredEvent : Event
    {
        public ChatRoomUserRegisteredEvent(Guid aggregateId, int userId, int chatRoomId)
        {
            UserId = userId;
            ChatRoomId = chatRoomId;
            AggregateId = aggregateId;
        }

        public int UserId { get; set; }
        public int ChatRoomId { get; set; }
    }
}