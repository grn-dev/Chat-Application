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

    public abstract class GetByIdToDestQueryHandler<TQuery, TDomain, TIdentifier, TDestination> : IQueryHandler<TQuery, TDestination>
  where TQuery : GetByIdToDestQuery<TDomain, TIdentifier, TDestination>
  where TDomain : BaseModel<TIdentifier>, IAggregateRoot

    {
        protected readonly ChatContext context;

        protected GetByIdToDestQueryHandler(ChatContext context)
        {
            this.context = context;
        }
        public virtual async Task<TDestination> Handle(TQuery request, CancellationToken cancellationToken)
        {
            return await context.Set<TDomain>()
                .AsQueryable()
                .Where(c => Comparer<TIdentifier>.Default.Compare(c.Id, request.Id) == 0)
                .ToDestination<TDestination>()
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
