using Chat.Domain.Common;
using NetDevPack.Domain;

namespace Chat.Domain.Models
{
    public class Attachment : BaseModel<int>, IAggregateRoot
    {
        private Attachment(string url,
            string fileName,
            string contentType,
            long size)
        {
            Url = url;
            FileName = fileName;
            ContentType = contentType;
            Size = size;
        }

        private Attachment()
        {
        }

        public string Url { get; private set; }
        public string FileName { get; private set; }
        public string ContentType { get; private set; }
        public long Size { get; private set; }
        public Message Message { get; private set; }

        public static Attachment Create(string url,
            string fileName,
            string contentType,
            long size)
        {
            return new Attachment(url, fileName, contentType, size);
        }

        public void Update(string url,
            string fileName,
            string contentType,
            long size)
        {
            Url = url;
            FileName = fileName;
            ContentType = contentType;
            Size = size;
            //MessageId = messageId;
        }
    }
}