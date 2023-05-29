using System.Threading.Tasks;
using Chat.Domain.Attributes;
using Chat.Domain.Commands.User;
using Chat.Infra.Data.Application.QueryHandler.RequestHandler;
using Chat.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Chat.Infra.Data.Application.CommandHandler.User
{
    [Bean]
    public class UpdateActivityCommandHandler : MyUpdateRequestHandler<
        UpdateActivityCommand, Chat.Domain.Models.User.User, int>
    {
        public UpdateActivityCommandHandler(ChatContext context) : base(context)
        {
        }

        protected override Task<Chat.Domain.Models.User.User> InitialDomain(UpdateActivityCommand request)
        {
            return _chatContext.Users.FirstAsync(x => x.UserId == request.UserId);
        }

        protected override Task UpdateFields(Chat.Domain.Models.User.User domain, UpdateActivityCommand request)
        {
            domain.UpdateLastActivity();
            return Task.CompletedTask;
        }
    }
}