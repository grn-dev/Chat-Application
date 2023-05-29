using Chat.Application.Configuration.Data.BasicQuery;
using Chat.Domain.Attributes;
using Chat.Domain.Common;
using Chat.Infra.Data.Application.QueryHandler.BasicQuery;
using Chat.Infra.Data.Context;
using NetDevPack.Domain;

namespace Chat.Infra.Data.Application.QueryHandler
{
    [Bean]
    public class GetSinglePredicateToDestGenericQueryHandler<TDomain, TIdentifier, TDestination>
        : GetSinglePredicateToDestQueryHandler<GetSinglePredicateToDestQuery<TDomain, TIdentifier, TDestination>, TDomain, TIdentifier, TDestination>
        where TDomain : BaseModel<TIdentifier>, IAggregateRoot
        where TDestination : class
    {
        public GetSinglePredicateToDestGenericQueryHandler(ChatContext context) : base(context)
        {
        }
    }
}