using Chat.Domain.Common;
using NetDevPack.Domain;

namespace Chat.Domain.Models
{
    public class Channel : BaseModel<int>, IAggregateRoot
    {
        private Channel(ChatRoom chatRoom)
        {
            ChatRoom = chatRoom;
        }

        private Channel()
        {
        }

        public ChatRoom ChatRoom { get; private set; }
        public int ChatRoomId { get; private set; }

        public static Channel Create(ChatRoom chatRoom)
        {
            return new Channel(chatRoom);
        }

        public void Update()
        {
        }

        public static Channel DeleteRegistered(int id) => new Channel() {Id = id};
    }
}