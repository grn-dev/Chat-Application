using NetDevPack.Messaging;
using System;

namespace Chat.Domain.Models.Events
{
    public class ChatRoomUserChangedStatusEvent : Event
    {
        public ChatRoomUserChangedStatusEvent(Guid aggregateId, int userId, int chatRoomId, ChatRoomUserStatus status)
        {
            UserId = userId;
            ChatRoomId = chatRoomId;
            Status = status;
            AggregateId = aggregateId;
        }

        public int UserId { get; set; }
        public int ChatRoomId { get; set; }
        public ChatRoomUserStatus Status { get; set; }
    }
}