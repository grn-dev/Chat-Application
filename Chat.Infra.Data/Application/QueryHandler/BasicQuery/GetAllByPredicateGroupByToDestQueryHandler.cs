using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Chat.Infra.Data.Domain.Pagination;
using Chat.Application.Configuration.Data;
using Chat.Application.Configuration.Data.BasicQuery;
using Chat.Domain.Common;
using Chat.Infra.Data.Context; 
using Microsoft.EntityFrameworkCore;
using NetDevPack.Domain;

namespace Chat.Infra.Data.Application.QueryHandler.BasicQuery
{
    public abstract class GetAllByPredicateGroupByToDestQueryHandler<TQuery, TDomain, TIdentifier, TDestination, TKey> :
        IQueryHandler<TQuery, List<TDestination>>
  where TQuery : GetAllByPredicateGroupByToDestQuery<TDomain, TIdentifier, TDestination, TKey>
  where TDomain : BaseModel<TIdentifier>, IAggregateRoot

    {
        private readonly ChatContext _context;

        protected GetAllByPredicateGroupByToDestQueryHandler(ChatContext context)
        {
            _context = context;
        }
        public virtual Task<List<TDestination>> Handle(TQuery request, CancellationToken cancellationToken)
        {
            return _context.Set<TDomain>()
                .AsQueryable()
                .Where(request.Predicate)
                .GroupBy(request.keySelector)
                .ToDestination<TDestination>()
                .ToListAsync(cancellationToken);
        }
    }
}
