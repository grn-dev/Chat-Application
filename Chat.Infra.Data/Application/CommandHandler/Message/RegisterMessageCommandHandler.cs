using System.Collections.Generic;
using Chat.Domain.Attributes;
using System.Threading.Tasks;
using Chat.Domain.Models;
using Chat.Infra.Data.Application.CommandHandler.RequestHandler;
using Chat.Infra.Data.Context;
using Chat.Infra.Data.Application.QueryHandler.RequestHandler;

namespace Chat.Domain.Commands.Message
{
    [Bean]
    public class RegisterMessageCommandHandler : MyCreateRequestHandler<
        RegisterMessageCommand, Models.Message, int>
    {
        private readonly IChatRoomMessageRuleChecker _chatRoomMessageRuleChecker;

        public RegisterMessageCommandHandler(ChatContext context,
            IChatRoomMessageRuleChecker chatRoomMessageRuleChecker) : base(context)
        {
            this._chatRoomMessageRuleChecker = chatRoomMessageRuleChecker;
        }


        protected override Task<Models.Message> InstantiateDomain(RegisterMessageCommand request)
        {
            var message = Models.Message.Create(request.Content, request.UserId,
                request.ParentId, request.AttachmentId, request.Type);

            // var chatRoomMessage = ChatRoomMessage.Create(request.ChatRoomId, message, request.UserId,
            //     _chatRoomMessageRuleChecker);
            // (message.ChatRoomMessages ??= new List<ChatRoomMessage>()).Add(chatRoomMessage);

            return Task.FromResult(message);
        }
    }
}