using Chat.Domain.Core.SeedWork;

namespace Chat.Enums
{
    public class UserLeftChatRoom : Enumeration
    {
        public static UserLeftChatRoom JoinedChatRoom =
            new UserLeftChatRoom(2, nameof(JoinedChatRoom), "Left ChatRoom");

        public static UserLeftChatRoom REGISTER_Body(string userName)
        {
            return new UserLeftChatRoom(2, nameof(JoinedChatRoom), " خارج شد " + userName);
        }

        public UserLeftChatRoom(int id, string name, string desc)
            : base(id, name, desc)
        {
        }
    }
}