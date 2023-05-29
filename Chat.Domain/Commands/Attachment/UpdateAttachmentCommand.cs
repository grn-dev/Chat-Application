using Chat.Domain.Attributes;
using Chat.Domain.Models;
using NetDevPack.Messaging;

namespace Chat.Domain.Commands.Attachment
{
     
    public class UpdateAttachmentCommand : BaseCommand<int>
    { 
        public string Url { get; set; }
		public string FileName { get; set; }
		public string ContentType { get; set; }
		public long Size { get; set; }
		public int MessageId { get; set; }
 
    }
}

