using Chat.Domain.Core.SeedWork;

namespace Chat.Enums
{
    public class UserJoinedChatRoom : Enumeration
    {
        public static UserJoinedChatRoom JoinedChatRoom =
            new UserJoinedChatRoom(1, nameof(JoinedChatRoom), "ملحق");

        public static UserJoinedChatRoom REGISTER_Body(string userName)
        {
            return new UserJoinedChatRoom(1, nameof(JoinedChatRoom), "ملحق شد" + userName);
        }

        public UserJoinedChatRoom(int id, string name, string desc)
            : base(id, name, desc)
        {
        }
    }
}