using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Chat.Application.Configuration.Data.BasicQuery;
using Chat.Domain.Attributes;
using Chat.Domain.Common;
using NetDevPack.Domain;
using Chat.Application.Configuration.Data;
using Chat.Infra.Data.Context;

namespace OnlineRegistration.Infra.Data.Application.QueryHandler
{
    [Bean]
    public class GetPredicateGenericQueryHandler<TDomain, TIdentifier>
        : IQueryHandler<GetPredicateQuery<TDomain, TIdentifier>, TDomain>
        where TDomain : BaseModel<TIdentifier>, IAggregateRoot
    {
        private readonly ChatContext _context;

        public GetPredicateGenericQueryHandler(ChatContext context)
        {
            _context = context;
        }

        public Task<TDomain> Handle(GetPredicateQuery<TDomain, TIdentifier> request,
            CancellationToken cancellationToken)
        {
            return _context.Set<TDomain>()
                .AsQueryable()
                .Where(request.Predicate)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}