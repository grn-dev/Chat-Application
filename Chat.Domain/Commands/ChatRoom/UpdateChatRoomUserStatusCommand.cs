using Chat.Domain.Models;

namespace Chat.Domain.Commands.ChatRoomUser
{
    public class UpdateChatRoomUserStatusCommand : BaseCommand<int>
    {
        public int UserId { get; set; }
        public int ChatRoomId { get; set; }
        public ChatRoomUserStatus Status { get; set; }
    }
}