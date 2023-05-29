using Chat.Domain.Models;

namespace Chat.Domain.Commands.Message
{
    public class RegisterMessageCommand : BaseCommand<int>
    {
        public string Content { get; set; }
        public int UserId { get; set; }
        public int? ParentId { get; set; }
        public int? AttachmentId { get; set; }
        public MessageType Type { get; set; }
    }
}