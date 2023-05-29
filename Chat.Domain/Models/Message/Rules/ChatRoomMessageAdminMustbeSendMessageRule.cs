using Chat.Domain.Core.SeedWork;

namespace Chat.Domain.Models.Rules
{
    public class ChatRoomMessageAdminMustbeSendMessageRule : IBusinessRule
    {
        private readonly IChatRoomMessageRuleChecker _chatRoomMessageRuleChecker;

        private readonly int _chatRoomId;
        private readonly int _senderId;
         
        

        public ChatRoomMessageAdminMustbeSendMessageRule(
            IChatRoomMessageRuleChecker chatRoomMessageRuleChecker, int chatRoomId, int senderId)
        {
            _chatRoomMessageRuleChecker = chatRoomMessageRuleChecker;
            _chatRoomId = chatRoomId;
            _senderId = senderId;
        }  
        public bool IsBroken() => _chatRoomMessageRuleChecker.ChatRoomMessageAdminMustbeSendMessage(_chatRoomId,_senderId).Result;

        public string Message => ChatRoomMessageAdminMustbeSendMessageErrorCode.ADMIN_MUSTBE_SEND_MESSAG.Desc;

        public string ErrorCode => ChatRoomMessageAdminMustbeSendMessageErrorCode.ADMIN_MUSTBE_SEND_MESSAG.Name.ToString();
    }
}

