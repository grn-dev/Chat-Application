using Chat.Domain.Attributes; 
using Chat.Infra.Data.Context;
using System.Threading.Tasks;
using Chat.Infra.Data.Application.QueryHandler.RequestHandler;


namespace Chat.Domain.Commands.Message
{
    [Bean]
    public class UpdateMessageCommandHandler : MyUpdateRequestHandler<
	    UpdateMessageCommand, Models.Message, int>
    {
         
        public UpdateMessageCommandHandler(ChatContext context) : base(context)
        { 
        }
 

        protected override Task UpdateFields(Models.Message domain, UpdateMessageCommand request)
        {
           //domain.Update(request.Content,
					// request.UserId,
					// request.ChatRoomMessageId,
					// request.DirectId,
					// request.ParentId,
					// request.AttachmentId,
					// request.MessageType);
            return Task.FromResult(0);
        }
    }
}

