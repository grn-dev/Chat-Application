using Chat.Domain.Core.SeedWork;

namespace Chat.Domain.Models
{
    public class ChatRoomNameMustBeUniqueErrorCode : Enumeration
    {
        public static ChatRoomNameMustBeUniqueErrorCode EXIST_NAME_CHATROOM = 
            new ChatRoomNameMustBeUniqueErrorCode(1, nameof(EXIST_NAME_CHATROOM), "قبلا با این نام ثبت شده است");


        public ChatRoomNameMustBeUniqueErrorCode(int id, string name, string desc)
            : base(id, name, desc)
        {

        }
    }
}

