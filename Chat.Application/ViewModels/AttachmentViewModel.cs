namespace Chat.Application.ViewModels
{
    public partial class AttachmentViewModel
    {
        public class AttachmentBaseExpose
        {
            public string Url { get; set; }
            public string FileName { get; set; }
            public string ContentType { get; set; }
        }

        public class AttachmentExpose
        {
            public int Id { get; set; }
            public string Url { get; set; }
            public string FileName { get; set; }
            public string ContentType { get; set; }
            public long Size { get; set; }
            public int MessageId { get; set; }
        }

        public class RegisterAttachmentToDirect
        {
            public string Url { get; set; }
            public string FileName { get; set; }
            public string ContentType { get; set; }
            public long Size { get; set; }
            public MessageViewModel.RegisterMessageToDirectAttachment Message { get; set; }
        }

        public class RegisterAttachmentToRoom
        {
            public string Url { get; set; }
            public string FileName { get; set; }
            public string ContentType { get; set; }
            public long Size { get; set; }
            public MessageViewModel.RegisterMessageToRoomAttachment Message { get; set; }
        }
    }
}