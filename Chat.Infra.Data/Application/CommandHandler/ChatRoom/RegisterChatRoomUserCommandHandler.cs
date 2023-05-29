using System.Threading.Tasks;
using Chat.Domain.Attributes;
using Chat.Domain.Commands.ChatRoom;
using Chat.Domain.Models;
using Chat.Infra.Data.Application.CommandHandler.RequestHandler;
using Chat.Infra.Data.Context;

namespace Chat.Infra.Data.Application.CommandHandler.ChatRoom
{
    [Bean]
    public class RegisterChatRoomUserCommandHandler : MyCreateRequestHandler<
        RegisterChatRoomUserCommand, ChatRoomUser, int>
    {
        private readonly IChatRoomUserRuleChecker chatRoomUserRuleChecker;

        public RegisterChatRoomUserCommandHandler(ChatContext context, IChatRoomUserRuleChecker chatRoomUserRuleChecker)
            : base(context)
        {
            this.chatRoomUserRuleChecker = chatRoomUserRuleChecker;
        }


        protected override Task<Chat.Domain.Models.ChatRoomUser> InstantiateDomain(RegisterChatRoomUserCommand request)
        {
            return Task.FromResult(Chat.Domain.Models.ChatRoomUser.CreateRegistered(
                request.UserId,
                request.ChatRoomId, request.Status, request.IsAdmin, chatRoomUserRuleChecker));
        }
    }
}