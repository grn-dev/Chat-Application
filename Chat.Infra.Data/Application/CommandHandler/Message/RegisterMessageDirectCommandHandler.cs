using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat.Domain.Attributes;
using Chat.Domain.Commands.Message;
using Chat.Domain.Models;
using Chat.Infra.Data.Application.CommandHandler.RequestHandler;
using Chat.Infra.Data.Application.QueryHandler.RequestHandler;
using Chat.Infra.Data.Context;

namespace Chat.Infra.Data.Application.CommandHandler.Message
{
    [Bean]
    public class RegisterMessageDirectCommandHandler : MyCreateRequestHandler<
        RegisterDirectMessageCommand, Chat.Domain.Models.DirectMessage, int>
    {
        public RegisterMessageDirectCommandHandler(ChatContext context) : base(context)
        {
        }

        protected override Task<Chat.Domain.Models.DirectMessage> InstantiateDomain(
            RegisterDirectMessageCommand request)
        {
            // var message = Chat.Domain.Models.Message.Create(request.Content, request.SenderId,
            //     request.ParentId, request.AttachmentId, request.Type);

            // var exist = ChatContext.Directs.Any(x =>
            //     (x.SenderId == request.SenderId && x.RecipientId == request.RecipientId) ||
            //     (x.SenderId == request.RecipientId && x.RecipientId == request.SenderId));
            //
            var directMessage = Chat.Domain.Models.DirectMessage.Create(request.DirectId, request.MessageId,
                request.SenderId, request.IsForwarded);

            return Task.FromResult(directMessage);
        }

        [Bean]
        public class RegisterChatRoomMessageCommandHandler : MyCreateRequestHandler<
            RegisterChatRoomMessageCommand, ChatRoomMessage, int>
        {
            private readonly IChatRoomMessageRuleChecker _chatRoomMessageRuleChecker;

            public RegisterChatRoomMessageCommandHandler(ChatContext context,
                IChatRoomMessageRuleChecker chatRoomMessageRuleChecker) : base(context)
            {
                _chatRoomMessageRuleChecker = chatRoomMessageRuleChecker;
            }


            protected override Task<ChatRoomMessage> InstantiateDomain(RegisterChatRoomMessageCommand request)
            {
                var chatRoomMessage = ChatRoomMessage.Create(request.ChatRoomId, request.MessageId,
                    request.SenderId, request.IsForwarded, _chatRoomMessageRuleChecker);

                return Task.FromResult(chatRoomMessage);
            }
        }
    }
}