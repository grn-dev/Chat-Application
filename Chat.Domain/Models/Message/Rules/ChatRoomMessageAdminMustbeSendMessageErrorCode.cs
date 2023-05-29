using Chat.Domain.Core.SeedWork;

namespace Chat.Domain.Models
{
    public class ChatRoomMessageAdminMustbeSendMessageErrorCode : Enumeration
    {
        public static ChatRoomMessageAdminMustbeSendMessageErrorCode ADMIN_MUSTBE_SEND_MESSAG = 
            new ChatRoomMessageAdminMustbeSendMessageErrorCode(1, nameof(ADMIN_MUSTBE_SEND_MESSAG), "قادر به ارسال پبام نمیباشید");


        public ChatRoomMessageAdminMustbeSendMessageErrorCode(int id, string name, string desc)
            : base(id, name, desc)
        {

        }
    }
}

