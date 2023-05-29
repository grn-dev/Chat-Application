using System;
using Chat.Domain.Common;
using Chat.Domain.Models.Events;
using NetDevPack.Domain;

namespace Chat.Domain.Models
{
    public class DirectMessage : BaseModel<int>, IAggregateRoot
    {
        private DirectMessage(int directId,
            int messageId,
            int senderId,
            bool isForwarded
        )
        {
            DirectId = directId;
            MessageId = messageId;
            SenderId = senderId;
            IsForwarded = isForwarded;
        }

        private DirectMessage()
        {
        }

        public int DirectId { get; private set; }
        public int MessageId { get; private set; }
        public int SenderId { get; private set; }
        public bool IsForwarded { get; private set; }
        public virtual Direct Direct { get; private set; }
        public virtual User.User Sender { get; private set; }
        public virtual Message Message { get; private set; }

        public static DirectMessage Create(int directId,
            int messageId,
            int senderId,
            bool isForwarded
        )
        {
            var direcMessage = new DirectMessage(directId, messageId, senderId, isForwarded);

            direcMessage.AddDomainEvent(
                new DirectMessageRegisteredEvent(Guid.NewGuid(), () => direcMessage.Id, directId));

            return direcMessage;
        }

        public void Update(int directId,
            int messageId,
            int senderId,
            bool isForwarded
        )
        {
            DirectId = directId;
            MessageId = messageId;
            SenderId = senderId;
            IsForwarded = isForwarded;
        }

        //public static DirectMessage DeleteRegistered(int id) => new DirectMessage() {Id = id};
    }
}