using Chat.Domain.Core.SeedWork;

namespace Chat.Domain.Models
{
    public class ChatRoomUserMustBeUniqueErrorCode : Enumeration
    {
        public static ChatRoomUserMustBeUniqueErrorCode EXIST_NAME_CHATROOMUSER =
            new ChatRoomUserMustBeUniqueErrorCode(1, nameof(EXIST_NAME_CHATROOMUSER), "قبلا کاربر در چت عضو شده است");


        public ChatRoomUserMustBeUniqueErrorCode(int id, string name, string desc)
            : base(id, name, desc)
        {

        }
    }
}

