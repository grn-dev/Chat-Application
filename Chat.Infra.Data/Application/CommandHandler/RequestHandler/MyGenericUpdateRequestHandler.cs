using System.Threading.Tasks;
using NetDevPack.Domain;
using Chat.Application.Configuration.Data.BasicCommand;
using Chat.Domain.Attributes;
using Chat.Domain.Common;
using Chat.Infra.Data.Context;

namespace Chat.Infra.Data.Application.QueryHandler.RequestHandler
{
    [Bean]
    public class
        MyGenericUpdateRequestHandler<TDomain, TIdentifier>
        : MyUpdateRequestHandler<GenericUpdateCommand<TDomain, TIdentifier>, TDomain, TIdentifier>
        where TDomain : BaseModel<TIdentifier>, IAggregateRoot
    {
        public MyGenericUpdateRequestHandler(ChatContext chatContext) : base(chatContext)
        {
        }

        protected override Task UpdateFields(TDomain domain, GenericUpdateCommand<TDomain, TIdentifier> request)
        {
            request.DomainFieldUpdater(domain);

            return Task.CompletedTask;
        }

        protected override Task<TDomain> InitialDomain(GenericUpdateCommand<TDomain, TIdentifier> request)
        {
            return request.DomainInitiator is not null
                ? request.DomainInitiator()
                : base.InitialDomain(request);
        }
    }
}