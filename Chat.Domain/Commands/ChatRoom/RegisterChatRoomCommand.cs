namespace Chat.Domain.Commands.ChatRoom
{
    public class RegisterChatRoomCommand : BaseCommandWithDomain<Models.ChatRoom>
    {
        public string Topic { get; set; }
        public string InviteCode { get; set; }
        public bool IsPrivate { get; set; }
    }
}