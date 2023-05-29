using Chat.Domain.Models.Ticket;

namespace Chat.Domain.Commands.Ticket
{
    public class ChangeTicketStatusCommand : BaseCommand<int>
    {
        public TicketStatus TicketStatus { get; set; }
    }
}

