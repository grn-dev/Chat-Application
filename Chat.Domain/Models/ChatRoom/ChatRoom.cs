using System;
using System.Collections.Generic;
using Chat.Domain.Common;
using Chat.Domain.Models.Events;
using Chat.Domain.Models.Rules;
using NetDevPack.Domain;

namespace Chat.Domain.Models
{
    public class ChatRoom : BaseModel<int>, IAggregateRoot
    {
        private ChatRoom(string name,
            string topic,
            string inviteCode,
            bool isPrivate,
            ChatRoomType type
        )
        {
            Name = name;
            Closed = false;
            Topic = topic;
            InviteCode = inviteCode;
            IsPrivate = isPrivate;
            LastNudged = DateTime.Now;
            Type = type;
        }

        private ChatRoom()
        {
        }

        public string Name { get; private set; }
        public bool Closed { get; private set; }
        public string Topic { get; private set; }
        public string InviteCode { get; private set; }
        public bool IsPrivate { get; private set; }
        public DateTime? LastNudged { get; private set; }
        public ChatRoomType Type { get; private set; }
        public virtual ICollection<ChatRoomMessage> ChatRoomMessages { get; private set; }
        public virtual ICollection<ChatRoomUser> ChatRoomUsers { get; private set; }
        public virtual ICollection<Channel> Channels { get; private set; }
        public virtual ICollection<Group> Groups { get; private set; }
        public virtual ICollection<Ticket.Ticket> Tickets { get; private set; }

        public static ChatRoom Create(IChatRoomRuleChecker chatRoomRuleChecker,
            string topic,
            string inviteCode,
            bool isPrivate,
            ChatRoomType type
        )
        {
            var name = Guid.NewGuid().ToString().Replace("-", "");
            CheckRule(new ChatRoomNameMustBeUniqueRule(chatRoomRuleChecker, name));
            var chatRoom = new ChatRoom(name, topic, inviteCode, isPrivate, type);
            chatRoom.AddDomainEvent(new ChatRoomRegisteredEvent(Guid.NewGuid(), () => chatRoom.Id, type));
            return chatRoom;
        }


        public void UpdateLastNudged()
        {
            LastNudged = DateTime.Now;
        }

        public void CloseChatRoom(bool closed) => Closed = closed;


        public void Update(
            string topic,
            string inviteCode,
            bool isPrivate
        )
        {
            Topic = topic;
            InviteCode = inviteCode;
            IsPrivate = isPrivate;
        }

        public static ChatRoom DeleteRegistered(int id) => new ChatRoom() {Id = id};
    }
}