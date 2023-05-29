using System.Threading.Tasks;

namespace Chat.Domain.Models
{
    public interface IChatRoomRuleChecker
    {
        Task<bool> ChatRoomNameMustBeUnique(string RoomName);
    }
}

