using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Domain;
using Chat.Domain.Commands;
using Chat.Domain.Common;
using Chat.Domain.Core.SeedWork;
using Chat.Infra.Data.Context;

namespace Chat.Infra.Data.Application.QueryHandler.RequestHandler
{
    public abstract class
        MyDeleteRequestHandler<TRequest, TDomain, TIdentifier>
        : NetDevPack.Messaging.CommandHandler, IRequestHandler<TRequest, CommandResponse<TIdentifier>>
        where TRequest : BaseCommand<TIdentifier>
        where TDomain : BaseModel<TIdentifier>, IAggregateRoot
    {
        protected readonly ChatContext ChatContext;
        protected readonly DbSet<TDomain> DbSet;

        protected MyDeleteRequestHandler(ChatContext acquirerContext)
        {
            ChatContext = acquirerContext;
            DbSet = acquirerContext.Set<TDomain>();
        }

        public virtual async Task<CommandResponse<TIdentifier>> Handle(TRequest request,
            CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return new CommandResponse<TIdentifier> { ValidationResult = request.ValidationResult };

            var domain = await InstantiateDomain(request);

            await WriteOnRepository(domain);

            return new CommandResponse<TIdentifier>()
            {
                ValidationResult = await Commit(ChatContext.UnitOfWork),
                Response = domain.Id
            };
        }

        protected abstract Task<TDomain> InstantiateDomain(TRequest request);

        protected virtual Task WriteOnRepository(TDomain domain)
        {
            DbSet.Remove(domain);

            return Task.CompletedTask;
        }
    }
    public abstract class
       MyBatchDeleteRequestHandler<TRequest, TDomain, TIdentifier>
       : NetDevPack.Messaging.CommandHandler, IRequestHandler<TRequest, CommandResponseBatch<TIdentifier>>
       where TRequest : BaseBatchCommand<TIdentifier>
       where TDomain : BaseModel<TIdentifier>, IAggregateRoot
    {
        protected readonly ChatContext ChatContext;
        protected readonly DbSet<TDomain> DbSet;

        protected MyBatchDeleteRequestHandler(ChatContext acquirerContext, DbSet<TDomain> dbSet)
        {
            ChatContext = acquirerContext;
            DbSet = dbSet;
        }

        public virtual async Task<CommandResponseBatch<TIdentifier>> Handle(TRequest request,
            CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return new CommandResponseBatch<TIdentifier> { ValidationResult = request.ValidationResult };

            var domain = await InstantiateDomain(request);

            await WriteOnRepository(domain);

            return new CommandResponseBatch<TIdentifier>()
            {
                ValidationResult = await Commit(ChatContext.UnitOfWork),
                Response = domain.Select(c => c.Id).ToList()
            };
        }

        protected abstract Task<List<TDomain>> InstantiateDomain(TRequest request);

        protected virtual Task WriteOnRepository(IList<TDomain> domain)
        {
            DbSet.RemoveRange(domain);

            return Task.CompletedTask;
        }
    }
}