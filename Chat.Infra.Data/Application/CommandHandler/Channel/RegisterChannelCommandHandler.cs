using Chat.Domain.Attributes;
using System.Threading.Tasks;
using Chat.Domain.Models;
using Chat.Infra.Data.Application.CommandHandler.RequestHandler;
using Chat.Infra.Data.Context; 

namespace Chat.Domain.Commands.Channel
{
    [Bean]
    public class RegisterChannelCommandHandler : MyCreateRequestHandlerWithDomain<
        RegisterChannelCommand, Models.Channel, int>
    {
        private readonly IChatRoomRuleChecker chatRoomRuleChecker;

        public RegisterChannelCommandHandler(ChatContext context, IChatRoomRuleChecker chatRoomRuleChecker) :
            base(context)
        {
            this.chatRoomRuleChecker = chatRoomRuleChecker;
        }


        protected override Task<Models.Channel> InstantiateDomain(RegisterChannelCommand request)
        {
            var chatroom = Models.ChatRoom.Create(chatRoomRuleChecker,
                request.Topic,
                request.InviteCode,
                request.IsPrivate, ChatRoomType.CHANNEL);

            return Task.FromResult(Models.Channel.Create(chatroom));
        }
    }
}