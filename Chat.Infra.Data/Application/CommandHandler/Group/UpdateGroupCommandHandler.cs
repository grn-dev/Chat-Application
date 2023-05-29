using System.Threading.Tasks;
using Chat.Domain.Attributes;
using Chat.Domain.Commands.Group;
using Chat.Infra.Data.Application.QueryHandler.RequestHandler;
using Chat.Infra.Data.Context;

namespace Chat.Infra.Data.Application.CommandHandler.Group
{
    [Bean]
    public class UpdateGroupCommandHandler : MyUpdateRequestHandler<
        UpdateGroupCommand, Chat.Domain.Models.Group, int>
    {
        public UpdateGroupCommandHandler(ChatContext context) : base(context)
        {
        }


        protected override Task UpdateFields(Chat.Domain.Models.Group domain, UpdateGroupCommand request)
        {
            domain.ChatRoom.Update(
                request.Topic,
                request.InviteCode,
                request.IsPrivate);
            return Task.FromResult(0);
        }
    }
}