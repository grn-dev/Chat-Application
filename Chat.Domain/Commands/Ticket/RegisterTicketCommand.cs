using Chat.Domain.Models.Ticket;

namespace Chat.Domain.Commands.Ticket
{
    public class RegisterTicketCommand : BaseCommandWithDomain<Models.Ticket.Ticket>
    {
        public int ChatRoomId { get; set; }
        public TicketStatus TicketStatus { get; set; }
        public string Subject { get; set; }
        public int? TicketCategoryId { get; set; }
        public TicketType? TicketType { get; set; }
        public Priority? Priority { get; set; }
    }
}