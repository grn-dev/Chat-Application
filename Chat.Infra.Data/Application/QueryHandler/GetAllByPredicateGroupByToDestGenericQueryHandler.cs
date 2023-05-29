using Chat.Application.Configuration.Data.BasicQuery;
using Chat.Domain.Attributes;
using Chat.Domain.Common;
using Chat.Infra.Data.Application.QueryHandler.BasicQuery;
using Chat.Infra.Data.Context;
using NetDevPack.Domain;

namespace Chat.Infra.Data.Application.QueryHandler
{
    [Bean]
    public class GetAllByPredicateGroupByToDestGenericQueryHandler<TDomain, TIdentifier, TDestination,Tkey>
        : GetAllByPredicateGroupByToDestQueryHandler<GetAllByPredicateGroupByToDestQuery<TDomain, TIdentifier, TDestination, Tkey>, TDomain, TIdentifier, TDestination, Tkey>
        where TDomain : BaseModel<TIdentifier>, IAggregateRoot
        where TDestination : class
    {
        public GetAllByPredicateGroupByToDestGenericQueryHandler(ChatContext context) : base(context)
        {
        }
    }
}