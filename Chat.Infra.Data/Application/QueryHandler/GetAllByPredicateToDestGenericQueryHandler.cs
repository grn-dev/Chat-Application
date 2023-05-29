using Chat.Application.Configuration.Data.BasicQuery;
using Chat.Domain.Attributes;
using Chat.Domain.Common;
using Chat.Infra.Data.Context;
using MakaTrip.Infra.Data.Application.QueryHandler.BasicQuery;
using NetDevPack.Domain;

namespace MakaTrip.Infra.Data.Application.QueryHandler
{
    [Bean]
    public class GetAllByPredicateToDestGenericQueryHandler<TDomain, TIdentifier, TDestination>
        : GetAllByPredicateToDestQueryHandler<GetAllByPredicateToDestQuery<TDomain, TIdentifier, TDestination>, TDomain, TIdentifier, TDestination>
        where TDomain : BaseModel<TIdentifier>, IAggregateRoot
        where TDestination : class
    {
        public GetAllByPredicateToDestGenericQueryHandler(ChatContext context) : base(context)
        {
        }
    }
}
