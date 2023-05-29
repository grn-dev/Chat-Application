using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Chat.Application.Configuration.Data;
using Chat.Application.Configuration.Data.BasicQuery;
using Chat.Domain.Common;
using Chat.Infra.Data.Context;
using Chat.Infra.Data.Domain.Pagination;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Domain;

namespace Chat.Infra.Data.Application.QueryHandler.BasicQuery
{
    public abstract class GetSinglePredicateToDestQueryHandler<TQuery, TDomain, TIdentifier, TDestination> :
        IQueryHandler<TQuery, TDestination>
  where TQuery : GetSinglePredicateToDestQuery<TDomain, TIdentifier, TDestination>
  where TDomain : BaseModel<TIdentifier>, IAggregateRoot

    {
        private readonly ChatContext _context;

        protected GetSinglePredicateToDestQueryHandler(ChatContext context)
        {
            _context = context;
        }
        public virtual Task<TDestination> Handle(TQuery request, CancellationToken cancellationToken)
        {
            return _context.Set<TDomain>()
                .AsQueryable()
                .Where(request.Predicate)
                .ToDestination<TDestination>()
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}
