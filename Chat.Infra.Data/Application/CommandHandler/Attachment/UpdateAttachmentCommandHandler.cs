using System.Threading.Tasks;
using Chat.Domain.Attributes;
using Chat.Domain.Commands.Attachment;
using Chat.Infra.Data.Application.QueryHandler.RequestHandler;
using Chat.Infra.Data.Context;

namespace Chat.Infra.Data.Application.CommandHandler.Attachment
{
    [Bean]
    public class UpdateAttachmentCommandHandler : MyUpdateRequestHandler<
        UpdateAttachmentCommand, Chat.Domain.Models.Attachment, int>
    {
        public UpdateAttachmentCommandHandler(ChatContext context) : base(context)
        {
        }


        protected override Task UpdateFields(Chat.Domain.Models.Attachment domain, UpdateAttachmentCommand request)
        {
            domain.Update(request.Url,
                request.FileName,
                request.ContentType,
                request.Size);
            return Task.FromResult(0);
        }
    }
}