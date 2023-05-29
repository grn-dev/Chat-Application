using System.Collections.Generic;
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
    public abstract class GetAllByPredicateToDestQueryHandler<TQuery, TDomain, TIdentifier, TDestination> :
        IQueryHandler<TQuery, List<TDestination>>
  where TQuery : GetAllByPredicateToDestQuery<TDomain, TIdentifier, TDestination>
  where TDomain : BaseModel<TIdentifier>, IAggregateRoot

    {
        private readonly ChatContext _context;

        protected GetAllByPredicateToDestQueryHandler(ChatContext context)
        {
            _context = context;
        }
        public virtual Task<List<TDestination>> Handle(TQuery request, CancellationToken cancellationToken)
        {
            return _context.Set<TDomain>()
                .AsQueryable()
                .Where(request.Predicate)
                .ToDestination<TDestination>()
                .ToListAsync(cancellationToken);
        }
    }
}
