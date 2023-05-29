using System.Threading.Tasks;
using Chat.Domain.Attributes;
using Chat.Domain.Commands.Attachment;
using Chat.Infra.Data.Application.CommandHandler.RequestHandler;
using Chat.Infra.Data.Context;

namespace Chat.Infra.Data.Application.CommandHandler.Attachment
{
    [Bean]
    public class RegisterAttachmentCommandHandler : MyCreateRequestHandler<
        RegisterAttachmentCommand, Chat.Domain.Models.Attachment, int>
    {
        public RegisterAttachmentCommandHandler(ChatContext context) : base(context)
        {
        }


        protected override Task<Chat.Domain.Models.Attachment> InstantiateDomain(RegisterAttachmentCommand request)
        {
            return Task.FromResult(Chat.Domain.Models.Attachment.Create(
                request.Url,
                request.FileName,
                request.ContentType,
                request.Size));
        }
    }
}