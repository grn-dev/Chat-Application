using Chat.Domain.Common;
using NetDevPack.Domain;

namespace Chat.Domain.Models.Ticket
{
    public class TicketHistory : BaseModel<int>, IAggregateRoot
    {
        public int TicketId { get; private set; }
        public TicketStatus TicketStatus { get; private set; }
        public Ticket Ticket { get; private set; }
    }
}