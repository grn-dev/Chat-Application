using System.Threading.Tasks;

namespace Chat.Domain.Models
{
    public interface IChatRoomMessageRuleChecker
    {
        Task<bool> ChatRoomMessageAdminMustbeSendMessage(int chatRoomId, int senderId); 
    }
}

