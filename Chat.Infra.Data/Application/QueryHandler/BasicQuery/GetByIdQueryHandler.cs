using System.Threading;
using System.Threading.Tasks;
using Chat.Application.Configuration.Data;
using Chat.Application.Configuration.Data.BasicQuery;
using Chat.Domain.Common;
using Chat.Infra.Data.Context;
using NetDevPack.Domain;

namespace Chat.Infra.Data.Application.QueryHandler.BasicQuery
{
    
    public abstract class GetByIdQueryHandler<TQuery, TDomain, TIdentifier> : IQueryHandler<TQuery,TDomain>
       where TQuery : GetByIdQuery<TDomain, TIdentifier>
       where TDomain : BaseModel<TIdentifier>, IAggregateRoot

    {
        protected readonly ChatContext context;

        protected GetByIdQueryHandler(ChatContext context)
        {
            context = context;
        }
        public virtual Task<TDomain> Handle(TQuery request, CancellationToken cancellationToken)
        {
            return context.Set<TDomain>().FindAsync(request.Id).AsTask();
        }
    }
}
