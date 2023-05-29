using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Chat.Application.Configuration.Data;
using Chat.Application.Configuration.Data.BasicQuery;
using Chat.Domain.Attributes;
using Chat.Domain.Common;
using Chat.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Domain;

namespace Chat.Infra.Data.Application.QueryHandler.BasicQuery
{
   
    
    [Bean]
    public class AnyByPredicateQueryHandler<TDomain, TIdentifier> : IQueryHandler<AnyByPredicateQuery<TDomain, TIdentifier>, bool>
        where TDomain : BaseModel<TIdentifier>, IAggregateRoot

    {
        private readonly ChatContext context;

        public AnyByPredicateQueryHandler(ChatContext context)
        {
            this.context = context;
        }
        public virtual async Task<bool> Handle(AnyByPredicateQuery<TDomain, TIdentifier> request, CancellationToken cancellationToken)
        {
            return await context.Set<TDomain>().
                 AsQueryable()
                .Where(request.Predicate)
                .AnyAsync();
        }

 
    }
}