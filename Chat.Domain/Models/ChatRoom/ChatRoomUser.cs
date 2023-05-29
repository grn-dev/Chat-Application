using System;
using Chat.Domain.Common;
using Chat.Domain.Models.Events;
using Chat.Domain.Models.Rules;
using NetDevPack.Domain;

namespace Chat.Domain.Models
{
    public class ChatRoomUser : BaseModel<int>, IAggregateRoot
    {
        public ChatRoomUser(int userId,
            int chatRoomId, ChatRoomUserStatus status, bool isAdmin)
        {
            UserId = userId;
            ChatRoomId = chatRoomId;
            Status = status;
            IsAdmin = isAdmin;
        }


        public int UserId { get; private set; }
        public int ChatRoomId { get; private set; }
        public ChatRoomUserStatus Status { get; private set; }
        public bool IsAdmin { get; private set; }

        public virtual User.User User { get; private set; }
        public virtual ChatRoom ChatRoom { get; private set; }

        public static ChatRoomUser CreateRegistered(int userId,
            int chatRoomId, ChatRoomUserStatus status, bool isAdmin, IChatRoomUserRuleChecker chatRoomUserRuleChecker)

        {
            CheckRule(new ChatRoomUserMustBeUniqueRule(chatRoomUserRuleChecker, userId, chatRoomId));
            var chatRoomUser = new ChatRoomUser(userId, chatRoomId, status, isAdmin);

            chatRoomUser.AddDomainEvent(new ChatRoomUserRegisteredEvent(Guid.NewGuid(), userId, chatRoomId));
            return chatRoomUser;
        }

        public void UpdateChatRoomUserStatus(ChatRoomUserStatus status)
        {
            this.AddDomainEvent(
                new ChatRoomUserChangedStatusEvent(Guid.NewGuid(), this.UserId, this.ChatRoomId, status));
            Status = status;
        }
        //public static ChatRoomUser DeleteRegistered(int id) => new ChatRoomUser() { Id = id };
    }
}