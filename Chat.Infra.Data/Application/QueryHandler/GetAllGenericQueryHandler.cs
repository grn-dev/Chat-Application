using Chat.Application.Configuration.Data.BasicQuery;
using Chat.Domain.Attributes;
using Chat.Domain.Common;
using Chat.Infra.Data.Application.QueryHandler.BasicQuery;
using Chat.Infra.Data.Context;
using NetDevPack.Domain;

namespace Chat.Infra.Data.Application.QueryHandler
{
    [Bean]
    public class GetAllGenericQueryHandler<TDomain, TIdentifier, TDestination>
        : GetAllQueryHandler<PagableQuery<TDomain, TIdentifier, TDestination>, TDomain, TIdentifier, TDestination>
        where TDomain : BaseModel<TIdentifier>, IAggregateRoot
        where TDestination : class
    {
        public GetAllGenericQueryHandler(ChatContext context) : base(context)
        {
        }
    }
}