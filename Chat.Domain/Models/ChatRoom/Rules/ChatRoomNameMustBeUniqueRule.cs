using Chat.Domain.Core.SeedWork;

namespace Chat.Domain.Models.Rules
{
    public class ChatRoomNameMustBeUniqueRule : IBusinessRule
    {
        private readonly IChatRoomRuleChecker _ChatRoomRuleChecker;

        private readonly string _chatRoomName;


        public ChatRoomNameMustBeUniqueRule(
            IChatRoomRuleChecker chatRoomRuleChecker, string chatRoomName)
        {
            _ChatRoomRuleChecker = chatRoomRuleChecker;
            _chatRoomName = chatRoomName;
        }
        public bool IsBroken() => _ChatRoomRuleChecker.ChatRoomNameMustBeUnique(_chatRoomName).Result;

        public string Message => ChatRoomNameMustBeUniqueErrorCode.EXIST_NAME_CHATROOM.Desc;

        public string ErrorCode => ChatRoomNameMustBeUniqueErrorCode.EXIST_NAME_CHATROOM.Name.ToString();
    }
}

