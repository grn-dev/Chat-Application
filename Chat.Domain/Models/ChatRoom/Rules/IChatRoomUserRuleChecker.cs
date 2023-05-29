using System.Threading.Tasks;

namespace Chat.Domain.Models
{
    public interface IChatRoomUserRuleChecker
    {
        Task<bool> ChatRoomUserMustBeUnique(int userId, int roomId);
    }
}

