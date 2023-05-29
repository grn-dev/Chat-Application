using System.Threading.Tasks;
using Chat.Domain.Attributes;
using Chat.Domain.Commands.Direct;
using Chat.Infra.Data.Application.CommandHandler.RequestHandler;
using Chat.Infra.Data.Context;

namespace Chat.Infra.Data.Application.CommandHandler.Direct
{
    [Bean]
    public class RegisterDirectCommandHandler : MyCreateRequestHandlerWithDomain<
        RegisterDirectCommand, Chat.Domain.Models.Direct, int>
    {
        public RegisterDirectCommandHandler(ChatContext context) : base(context)
        {
        }

        protected override Task<Chat.Domain.Models.Direct> InstantiateDomain(RegisterDirectCommand request)
        {
            return Task.FromResult(Chat.Domain.Models.Direct.Create());
        }
    }
}