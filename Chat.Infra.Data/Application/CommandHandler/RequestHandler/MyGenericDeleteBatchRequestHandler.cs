using System.Collections.Generic;
using System.Threading.Tasks;
using NetDevPack.Domain;
using Chat.Domain.Attributes;
using Chat.Domain.Common;
using Chat.Infra.Data.Context; 
using Chat.Application.Configuration.Data.BasicCommand;

namespace Chat.Infra.Data.Application.QueryHandler.RequestHandler
{
    [Bean]
    public class
        MyGenericDeleteBatchRequestHandler<TDomain, TIdentifier>
        : MyBatchDeleteRequestHandler<GenericDeleteBatchCommand<TDomain, TIdentifier>, TDomain, TIdentifier>
        where TDomain : BaseModel<TIdentifier>, IAggregateRoot
    {
        public MyGenericDeleteBatchRequestHandler(ChatContext acquirerContext)
            : base(acquirerContext, acquirerContext.Set<TDomain>())
        {
        }

        protected override Task<List<TDomain>> InstantiateDomain(GenericDeleteBatchCommand<TDomain, TIdentifier> request)
        {
            return request.DomainInitiator();
        }
    }

    [Bean]
    public class
        MyGenericDeleteRequestHandler<TDomain, TIdentifier>
        : MyDeleteRequestHandler<GenericDeleteCommand<TDomain, TIdentifier>, TDomain, TIdentifier>
        where TDomain : BaseModel<TIdentifier>, IAggregateRoot
    {
        public MyGenericDeleteRequestHandler(ChatContext acquirerContext) : base(acquirerContext)
        {
        }

        protected override Task<TDomain> InstantiateDomain(GenericDeleteCommand<TDomain, TIdentifier> request)
        {
            return request.DomainInitiator();
        }
    }
}