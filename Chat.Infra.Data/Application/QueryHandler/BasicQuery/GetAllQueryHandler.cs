using System.Threading;
using System.Threading.Tasks;
using Garnet.Standard.Pagination;
using Chat.Application.Configuration.Data;
using Chat.Application.Configuration.Data.BasicQuery;
using Chat.Domain.Common;
using Chat.Infra.Data.Context;
using NetDevPack.Domain;
using Chat.Infra.Data.Domain.Pagination;

namespace Chat.Infra.Data.Application.QueryHandler.BasicQuery
{
    public abstract class GetAllQueryHandler<TQuery, TDomain, TIdentifier, TDestination> : IQueryHandler<TQuery, IPagedElements<TDestination>>
        where TQuery : PagableQuery<TDomain, TIdentifier, TDestination>
        where TDomain : BaseModel<TIdentifier>, IAggregateRoot
        where TDestination : class

    {
        protected readonly ChatContext _context;

        protected GetAllQueryHandler(ChatContext context)
        {
            _context = context;
        }

        public virtual Task<IPagedElements<TDestination>> Handle(TQuery request, CancellationToken cancellationToken)
        {
            return _context.Set<TDomain>().UsePageableUseMapperAsync<TDomain, TDestination>(request.Pagination);
        }
    }
}