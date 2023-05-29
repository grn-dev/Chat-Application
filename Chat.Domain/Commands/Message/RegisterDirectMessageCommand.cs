namespace Chat.Domain.Commands.Message
{
    public class RegisterDirectMessageCommand : BaseCommand<int>
    {
        public int DirectId { get; set; }
        public int MessageId { get; set; }
        public int SenderId { get; set; }
        public bool IsForwarded { get; set; }
    }

    public class RegisterChatRoomMessageCommand : BaseCommand<int>
    {
        public int ChatRoomId { get; set; }
        public int MessageId { get; set; }
        public int SenderId { get; set; }
        public bool IsForwarded { get; set; }
    }
}