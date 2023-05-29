using System;
using Chat.Domain.Common;
using Chat.Domain.Models.Events;
using Chat.Domain.Models.Rules;
using NetDevPack.Domain;

namespace Chat.Domain.Models
{
    public class ChatRoomMessage : BaseModel<int>, IAggregateRoot
    {
        private ChatRoomMessage(int chatRoomId,
            int messageId, int senderId, bool isForwarded)
        {
            ChatRoomId = chatRoomId;
            MessageId = messageId;
            SenderId = senderId;
            IsForwarded = isForwarded;
        }

        public int ChatRoomId { get; private set; }
        public int MessageId { get; private set; }
        public int SenderId { get; private set; }
        public bool IsForwarded { get; private set; }
        public virtual ChatRoom ChatRoom { get; private set; }
        public virtual User.User Sender { get; private set; }
        public virtual Message Message { get; private set; }

        public static ChatRoomMessage Create(int chatRoomId, int messageId, int senderId, bool isForwarded,
            IChatRoomMessageRuleChecker chatRoomMessageRuleChecker)
        {
            CheckRule(new ChatRoomMessageAdminMustbeSendMessageRule(chatRoomMessageRuleChecker, chatRoomId, senderId));

            var chatRoomMessage = new ChatRoomMessage(chatRoomId, messageId, senderId, isForwarded);

            chatRoomMessage.AddDomainEvent(
                new ChatRoomMessageRegisteredEvent(Guid.NewGuid(), () => chatRoomMessage.Id, chatRoomId));

            return chatRoomMessage;
        }
    }
}