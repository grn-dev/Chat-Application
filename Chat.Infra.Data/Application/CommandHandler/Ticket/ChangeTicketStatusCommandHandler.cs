using Chat.Domain.Attributes;
using Chat.Domain.Commands.Ticket;
using Chat.Infra.Data.Application.QueryHandler.RequestHandler;
using Chat.Infra.Data.Context;
using System.Threading.Tasks;

namespace Chat.Infra.Data.Application.CommandHandler.Ticket
{
    [Bean]
    public class ChangeTicketStatusCommandHandler : MyUpdateRequestHandler<
            ChangeTicketStatusCommand, Chat.Domain.Models.Ticket.Ticket, int>
    {

        public ChangeTicketStatusCommandHandler(ChatContext context) : base(context)
        {
        }
        protected override Task UpdateFields(Chat.Domain.Models.Ticket.Ticket domain, ChangeTicketStatusCommand request)
        {
            domain.ChangeStatus(request.TicketStatus);
            return Task.FromResult(0);
        }
    }
}

