using System.Threading.Tasks;
using Chat.Domain.Attributes;
using Chat.Domain.Commands.ChatRoomUser;
using Chat.Infra.Data.Application.QueryHandler.RequestHandler;
using Chat.Infra.Data.Context;

namespace Chat.Infra.Data.Application.CommandHandler.ChatRoom
{
    [Bean]
    public class UpdateLastNudgedCommandHandler : MyUpdateRequestHandler<
        UpdateLastNudgedCommand, Chat.Domain.Models.ChatRoom, int>
    {
        public UpdateLastNudgedCommandHandler(ChatContext context)
            : base(context)
        {
        }

        protected override Task UpdateFields(Chat.Domain.Models.ChatRoom domain, UpdateLastNudgedCommand request)
        {
            domain.UpdateLastNudged();
            return Task.CompletedTask;
        }
    }
}