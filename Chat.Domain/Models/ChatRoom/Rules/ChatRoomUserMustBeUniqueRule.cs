using Chat.Domain.Core.SeedWork;

namespace Chat.Domain.Models.Rules
{
    public class ChatRoomUserMustBeUniqueRule : IBusinessRule
    {
        private readonly IChatRoomUserRuleChecker _chatRoomUserRuleChecker;

        private readonly int _userId;
        private readonly int _chatRoomId;


        public ChatRoomUserMustBeUniqueRule(
            IChatRoomUserRuleChecker chatRoomUserRuleChecker, int userId, int chatRoomId)
        {
            _chatRoomUserRuleChecker = chatRoomUserRuleChecker;
            _userId = userId;
            _chatRoomId = chatRoomId;
        } 
        public bool IsBroken() => _chatRoomUserRuleChecker.ChatRoomUserMustBeUnique(_userId, _chatRoomId).Result;

        public string Message => ChatRoomUserMustBeUniqueErrorCode.EXIST_NAME_CHATROOMUSER.Desc;

        public string ErrorCode => ChatRoomUserMustBeUniqueErrorCode.EXIST_NAME_CHATROOMUSER.Name.ToString();
    }
}

