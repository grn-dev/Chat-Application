using Chat.Domain.Common;
using NetDevPack.Domain;

namespace Chat.Domain.Models
{
    public class Group : BaseModel<int>, IAggregateRoot
    {
        private Group(ChatRoom chatRoom)
        {
            ChatRoom = chatRoom;
        }

        private Group()
        {
        }

        public ChatRoom ChatRoom { get; private set; }
        public int ChatRoomId { get; private set; }

        public static Group Create(ChatRoom chatRoom)
        {
            return new Group(chatRoom);
        }

        public void Update()
        {
        }

        public static Group DeleteRegistered(int id) => new Group() {Id = id};
    }
}