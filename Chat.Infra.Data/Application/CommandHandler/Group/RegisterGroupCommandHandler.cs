using Chat.Domain.Attributes;
using System.Threading.Tasks;
using Chat.Domain.Models;
using Chat.Infra.Data.Application.CommandHandler.RequestHandler;
using Chat.Infra.Data.Context;
using Chat.Infra.Data.Application.QueryHandler.RequestHandler;

namespace Chat.Domain.Commands.Group
{
    [Bean]
    public class RegisterGroupCommandHandler : MyCreateRequestHandlerWithDomain<
        RegisterGroupCommand, Models.Group, int>
    {
        private readonly IChatRoomRuleChecker chatRoomRuleChecker;

        public RegisterGroupCommandHandler(ChatContext context, IChatRoomRuleChecker chatRoomRuleChecker) :
            base(context)
        {
            this.chatRoomRuleChecker = chatRoomRuleChecker;
        }


        protected override Task<Models.Group> InstantiateDomain(RegisterGroupCommand request)
        {
            var chatroom = Models.ChatRoom.Create(chatRoomRuleChecker,
                request.Topic,
                request.InviteCode,
                request.IsPrivate, ChatRoomType.GROUP);

            return Task.FromResult(Models.Group.Create(chatroom)); 
        }
    }
}