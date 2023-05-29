using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Chat.Application.Configuration.Data.BasicQuery;
using Chat.Domain.Common;
using NetDevPack.Domain;
using Chat.Application.Configuration.Data;
using Chat.Infra.Data.Context;

namespace OnlineRegistration.Infra.Data.Application.QueryHandler
{
    public class GetPredicateGenericQueryTrackingHandler<TDomain, TIdentifier>
        : IQueryHandler<GetPredicateQueryTracking<TDomain, TIdentifier>, TDomain>
        where TDomain : BaseModel<TIdentifier>, IAggregateRoot
    {
        private readonly ChatContext _context;

        public GetPredicateGenericQueryTrackingHandler(ChatContext context)
        {
            _context = context;
        }

        public Task<TDomain> Handle(GetPredicateQueryTracking<TDomain, TIdentifier> request,
            CancellationToken cancellationToken)
        {
            return _context.Set<TDomain>()
                .AsQueryable().AsTracking()
                .Where(request.Predicate)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}