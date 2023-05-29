using System.Threading.Tasks;
using Chat.Domain.Attributes;
using Chat.Infra.Data.Application.QueryHandler.RequestHandler;
using Chat.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Chat.Domain.Commands.ChatRoomUser
{
    [Bean]
    public class UpdateChatRoomUserStatusCommandHandler : MyUpdateRequestHandler<
        UpdateChatRoomUserStatusCommand, Models.ChatRoomUser, int>
    {
        public UpdateChatRoomUserStatusCommandHandler(ChatContext context)
            : base(context)
        {
        }

        protected override Task<Models.ChatRoomUser> InitialDomain(UpdateChatRoomUserStatusCommand request)
        {
            return _chatContext.ChatRoomUsers.FirstOrDefaultAsync(x =>
                x.UserId == request.UserId &&
                x.ChatRoomId == request.ChatRoomId);
        }

        protected override Task UpdateFields(Models.ChatRoomUser domain, UpdateChatRoomUserStatusCommand request)
        {
            domain.UpdateChatRoomUserStatus(request.Status);
            return Task.CompletedTask;
        }
    }
}