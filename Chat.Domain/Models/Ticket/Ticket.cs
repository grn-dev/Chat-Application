using Chat.Domain.Common;
using NetDevPack.Domain;
using System.Collections.Generic;

namespace Chat.Domain.Models.Ticket
{
    public class Ticket : BaseModel<int>, IAggregateRoot
    {
        private Ticket(int chatRoomId,
            string subject,
            int? ticketCategoryId,
            TicketType? ticketType,
            Priority? priority
        )
        {
            ChatRoomId = chatRoomId;
            TicketStatus = TicketStatus.OPEN;
            Subject = subject;
            TicketCategoryId = ticketCategoryId;
            TicketType = ticketType;
            Priority = priority;
        }

        private Ticket()
        {
        }

        public int ChatRoomId { get; private set; }
        public TicketStatus TicketStatus { get; private set; }
        public string Subject { get; private set; }
        public int? TicketCategoryId { get; private set; }
        public TicketType? TicketType { get; private set; }
        public Priority? Priority { get; private set; }

        public TicketCategory TicketCategory { get; private set; }
        public ChatRoom ChatRoom { get; private set; }
        public ICollection<TicketHistory> TicketHistories { get; private set; }

        public void ChangeStatus(TicketStatus ticketStatus)
        {
            TicketStatus = ticketStatus;
        }

        public static Ticket Create(int chatroomId,
            TicketStatus ticketStatus,
            string subject,
            int? ticketCategoryId,
            TicketType? ticketType,
            Priority? priority
        )
        {
            return new Ticket(chatroomId, subject, ticketCategoryId, ticketType, priority);
        }
    }
}