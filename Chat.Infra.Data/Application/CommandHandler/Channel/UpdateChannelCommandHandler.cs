using Chat.Domain.Attributes;
using Chat.Infra.Data.Context;
using System.Threading.Tasks;
using Chat.Infra.Data.Application.QueryHandler.RequestHandler;


namespace Chat.Domain.Commands.Channel
{
    [Bean]
    public class UpdateChannelCommandHandler : MyUpdateRequestHandler<
        UpdateChannelCommand, Models.Channel, int>
    {
        public UpdateChannelCommandHandler(ChatContext context) : base(context)
        {
        }


        protected override Task UpdateFields(Models.Channel domain, UpdateChannelCommand request)
        {
            domain.ChatRoom.Update(
                request.Topic,
                request.InviteCode,
                request.IsPrivate);
            return Task.FromResult(0);
        }
    }
}