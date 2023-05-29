using System.Threading.Tasks;
using Chat.Domain.Attributes;
using Chat.Domain.Commands.Ticket;
using Chat.Infra.Data.Application.CommandHandler.RequestHandler;
using Chat.Infra.Data.Context;

namespace Chat.Infra.Data.Application.CommandHandler.Ticket
{
    [Bean]
    public class RegisterTicketCommandHandler : MyCreateRequestHandlerWithDomain<
        RegisterTicketCommand, Chat.Domain.Models.Ticket.Ticket, int>
    {
        public RegisterTicketCommandHandler(ChatContext context) : base(context)
        {
        }


        protected override Task<Chat.Domain.Models.Ticket.Ticket> InstantiateDomain(RegisterTicketCommand request)
        {
            return Task.FromResult(Chat.Domain.Models.Ticket.Ticket.Create(
                request.ChatRoomId,
                request.TicketStatus,
                request.Subject,
                request.TicketCategoryId,
                request.TicketType,
                request.Priority));
        }
    }
}