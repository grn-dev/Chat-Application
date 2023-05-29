using System.Threading.Tasks;
using NetDevPack.Domain;
using Chat.Application.Configuration.Data.BasicCommand;
using Chat.Domain.Attributes;
using Chat.Domain.Common;
using Chat.Infra.Data.Application.CommandHandler.RequestHandler;
using Chat.Infra.Data.Context;

namespace Chat.Infra.Data.Application.QueryHandler.RequestHandler
{
    [Bean]
    public class
        MyGenericCreateRequestHandler<TDomain, TIdentifier>
        : MyCreateRequestHandler<GenericCreateCommand<TDomain, TIdentifier>, TDomain, TIdentifier>
        where TDomain : BaseModel<TIdentifier>, IAggregateRoot
    {
        public MyGenericCreateRequestHandler(ChatContext acquirerContext) : base(
            acquirerContext)
        {
        }

        protected override Task<TDomain> InstantiateDomain(GenericCreateCommand<TDomain, TIdentifier> request)
        {
            return request.DomainInitiator();
        }
    }
}