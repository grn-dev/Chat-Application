using System.Threading.Tasks;
using Chat.Domain.Attributes;
using Chat.Domain.Commands.Direct;
using Chat.Domain.Models;
using Chat.Infra.Data.Application.CommandHandler.RequestHandler;
using Chat.Infra.Data.Application.QueryHandler.RequestHandler;
using Chat.Infra.Data.Context;

namespace Chat.Infra.Data.Application.CommandHandler.Direct
{
    [Bean]
    public class RegisterDirectUserCommandHandler : MyCreateRequestHandler<
        RegisterDirectUserCommand, DirectUser, int>
    {
        public RegisterDirectUserCommandHandler(ChatContext context) : base(context)
        {
        }


        protected override Task<DirectUser> InstantiateDomain(RegisterDirectUserCommand request)
        {
            return Task.FromResult(DirectUser.Create(
                request.UserId,
                request.DirectId));
        }
    }
}