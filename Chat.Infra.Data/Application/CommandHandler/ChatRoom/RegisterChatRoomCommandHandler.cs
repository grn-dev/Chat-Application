using System.Threading.Tasks;
using Chat.Domain.Attributes;
using Chat.Domain.Commands.ChatRoom;
using Chat.Domain.Models;
using Chat.Infra.Data.Application.CommandHandler.RequestHandler;
using Chat.Infra.Data.Context;

namespace Chat.Infra.Data.Application.CommandHandler.ChatRoom
{
    [Bean]
    public class RegisterChatRoomCommandHandler : MyCreateRequestHandlerWithDomain<
        RegisterChatRoomCommand, Chat.Domain.Models.ChatRoom, int>
    {
        private readonly IChatRoomRuleChecker _chatRoomRuleChecker;

        public RegisterChatRoomCommandHandler(ChatContext context, IChatRoomRuleChecker chatRoomRuleChecker)
            : base(context)
        {
            _chatRoomRuleChecker = chatRoomRuleChecker;
        }

        protected override Task<Chat.Domain.Models.ChatRoom> InstantiateDomain(RegisterChatRoomCommand request)
        {
            var chatroom = Chat.Domain.Models.ChatRoom.Create(_chatRoomRuleChecker,
                request.Topic,
                request.InviteCode,
                request.IsPrivate, ChatRoomType.TICKET);

            return Task.FromResult(chatroom);
        }
    }
}