using System.Collections.Generic;
using Chat.Domain.Common;
using NetDevPack.Domain;

namespace Chat.Domain.Models
{
    public class Message : BaseModel<int>, IAggregateRoot
    {
        private Message(string content,
            int userId,
            int? parentId,
            int? attachmentId,
            MessageType type
        )
        {
            Content = content;
            UserId = userId;
            ParentId = parentId;
            AttachmentId = attachmentId;
            Type = type;
        }

        private Message()
        {
        }

        public string Content { get; private set; }
        public int UserId { get; private set; }
        public int? ParentId { get; private set; }
        public int? AttachmentId { get; private set; }
        public MessageType Type { get; private set; }

        //public int? ChatRoomMessageId { get; private set; }
        //public int? DirectId { get; private set; }

        //public ChatRoomMessage ChatRoomMessage { get; private set; }
        //public Direct Direct { get; private set; }
        //public Message Parent { get; private set; }
        //public User User { get; private set; }
        public Attachment Attachment { get; private set; }

        public virtual Message Parent { get; set; }

        public virtual User.User User { get; set; }

        //public virtual ICollection<Attachment> Attachments { get; set; }
        public virtual ICollection<ChatRoomMessage> ChatRoomMessages { get; set; }
        public virtual ICollection<DirectMessage> DirectMessages { get; set; }
        public virtual ICollection<Message> Children { get; set; }

        public static Message Create(string content,
            int userId,
            int? parentId,
            int? attachmentId,
            MessageType type
        )
        {
            var message = new Message(content, userId, parentId, attachmentId, type);
            //message.AddDomainEvent(new MessageRegisteredEvent(Guid.NewGuid(), () => message.Id, content,userId));
            return message;
        }

        public void Update(string content,
            int userId,
            int? chatRoomMessageId,
            int? directId,
            int? parentId,
            //int? attachmentId,
            MessageType messageType
        )
        {
            Content = content;
            UserId = userId;
            ParentId = parentId;
            //AttachmentId = attachmentId;
            Type = messageType;
        }

        public static Message DeleteRegistered(int id) => new Message() {Id = id};
    }
}