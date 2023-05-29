using Chat.Domain.Models;

namespace Chat.Domain.Commands.ChatRoom
{
    public class RegisterChatRoomUserCommand : BaseCommand<int>
    {
        public int UserId { get; set; }
        public int ChatRoomId { get; set; }
        public ChatRoomUserStatus Status { get; set; }
        public bool IsAdmin { get; set; }
    }
}