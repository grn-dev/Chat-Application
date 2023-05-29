using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Chat.Domain.Commands;
using Chat.Domain.Common;
using Chat.Domain.Core.SeedWork;
using Chat.Infra.Data.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Domain;

namespace Chat.Infra.Data.Application.CommandHandler.RequestHandler
{
    public abstract class
        MyCreateRequestHandler<TRequest, TDomain, TIdentifier>
        : NetDevPack.Messaging.CommandHandler, IRequestHandler<TRequest, CommandResponse<TIdentifier>>
        where TRequest : BaseCommand<TIdentifier>
        where TDomain : BaseModel<TIdentifier>, IAggregateRoot
    {
        protected readonly ChatContext ChatContext;
        protected readonly DbSet<TDomain> DbSet;

        protected MyCreateRequestHandler(ChatContext context)
        {
            ChatContext = context;
            DbSet = context.Set<TDomain>();
        }

        public virtual Task<CommandResponse<TIdentifier>> Handle(TRequest request,
            CancellationToken cancellationToken)
        {
            return CustomHandler<TIdentifier, TDomain, TRequest>(request, ChatContext, InstantiateDomain,
                WriteOnRepository);
        }

        public async Task<CommandResponse<TId>> CustomHandler<TId, TEntity, TReq>(TReq request,
            ChatContext context,
            Func<TReq, Task<TEntity>> domainProvider,
            Func<TEntity, TReq, Task> repositoryWriter)
            where TReq : BaseCommand<TId>
            where TEntity : BaseModel<TId>, IAggregateRoot
        {
            if (!request.IsValid())
                return new CommandResponse<TId> {ValidationResult = request.ValidationResult};

            var domain = await domainProvider(request);

            await repositoryWriter(domain, request);

            return new CommandResponse<TId>()
            {
                ValidationResult = await Commit(context.UnitOfWork),
                Response = domain.Id
            };
         }

        protected abstract Task<TDomain> InstantiateDomain(TRequest request);

        protected virtual Task WriteOnRepository(TDomain domain, TRequest request)
        {
            return DbSet.AddAsync(domain).AsTask();
        }
    }

    public abstract class
        MyCreateRequestHandlerWithDomain<TRequest, TDomain, TIdentifier>
        : NetDevPack.Messaging.CommandHandler, IRequestHandler<TRequest, CommandResponse<TDomain>>
        where TRequest : BaseCommandWithDomain<TDomain>
        where TDomain : BaseModel<TIdentifier>, IAggregateRoot
    {
        protected readonly ChatContext ChatContext;
        protected readonly DbSet<TDomain> DbSet;

        protected MyCreateRequestHandlerWithDomain(ChatContext context)
        {
            ChatContext = context;
            DbSet = ChatContext.Set<TDomain>();
        }

        public virtual Task<CommandResponse<TDomain>> Handle(TRequest request,
            CancellationToken cancellationToken)
        {
            return CustomHandler<TIdentifier, TDomain, TRequest>(request, ChatContext, InstantiateDomain,
                WriteOnRepository);
        }

        public async Task<CommandResponse<TEntity>> CustomHandler<TId, TEntity, TReq>(TReq request,
            ChatContext context,
            Func<TReq, Task<TEntity>> domainProvider,
            Func<TEntity, TReq, Task> repositoryWriter)
            where TReq : BaseCommandWithDomain<TEntity>
            where TEntity : BaseModel<TId>, IAggregateRoot
        {
            if (!request.IsValid())
                return new CommandResponse<TEntity> {ValidationResult = request.ValidationResult};

            var domain = await domainProvider(request);

            await repositoryWriter(domain, request);

            return new CommandResponse<TEntity>()
            {
                ValidationResult = await Commit(context.UnitOfWork),
                Response = domain
            };
        }

        protected abstract Task<TDomain> InstantiateDomain(TRequest request);

        protected virtual Task WriteOnRepository(TDomain domain, TRequest request)
        {
            return DbSet.AddAsync(domain).AsTask();
        }
    }

    public abstract class
        MyCreateBatchRequestHandler<TRequest, TDomain, TIdentifier>
        : NetDevPack.Messaging.CommandHandler, IRequestHandler<TRequest, CommandResponseBatch<TIdentifier>>
        where TRequest : BaseBatchCommand<TIdentifier>
        where TDomain : BaseModel<TIdentifier>, IAggregateRoot
    {
        protected readonly ChatContext ChatContext;
        protected readonly DbSet<TDomain> DbSet;

        protected MyCreateBatchRequestHandler(ChatContext acquirerContext)
        {
            ChatContext = acquirerContext;
            DbSet = acquirerContext.Set<TDomain>();
        }

        public virtual async Task<CommandResponseBatch<TIdentifier>> Handle(TRequest request,
            CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return new CommandResponseBatch<TIdentifier> {ValidationResult = request.ValidationResult};

            var domain = await InstantiateDomain(request);

            await WriteOnRepository(domain);

            return new CommandResponseBatch<TIdentifier>()
            {
                ValidationResult = await Commit(ChatContext.UnitOfWork),
                Response = domain.Select(c => c.Id).ToList()
            };
        }

        protected abstract Task<IList<TDomain>> InstantiateDomain(TRequest request);

        protected virtual Task WriteOnRepository(IList<TDomain> domain)
        {
            DbSet.AddRangeAsync(domain);

            return Task.CompletedTask;
        }
    }

    public abstract class
        MyCreateRequestHandler<TRequest1, TRequest2, TDomain, TIdentifier>
        : MyCreateRequestHandler<TRequest1, TDomain, TIdentifier>,
            IRequestHandler<TRequest2, CommandResponse<TIdentifier>>
        where TRequest1 : BaseCommand<TIdentifier>
        where TDomain : BaseModel<TIdentifier>, IAggregateRoot
        where TRequest2 : BaseCommand<TIdentifier>
    {
        protected MyCreateRequestHandler(ChatContext context) : base(context)
        {
        }

        public Task<CommandResponse<TIdentifier>> Handle(TRequest2 request, CancellationToken cancellationToken)
        {
            return CustomHandler<TIdentifier, TDomain, TRequest2>(request, ChatContext, InstantiateDomain,
                WriteOnRepository);
        }

        protected abstract Task<TDomain> InstantiateDomain(TRequest2 request);

        protected virtual Task WriteOnRepository(TDomain domain, TRequest2 request)
        {
            return DbSet.AddAsync(domain).AsTask();
        }
    }

    public abstract class
        MyCreateRequestHandler<TRequest1, TRequest2, TRequest3, TDomain, TIdentifier>
        : MyCreateRequestHandler<TRequest1, TRequest2, TDomain, TIdentifier>,
            IRequestHandler<TRequest3, CommandResponse<TIdentifier>>
        where TRequest1 : BaseCommand<TIdentifier>
        where TDomain : BaseModel<TIdentifier>, IAggregateRoot
        where TRequest2 : BaseCommand<TIdentifier>
        where TRequest3 : BaseCommand<TIdentifier>
    {
        protected MyCreateRequestHandler(ChatContext acquirerContext) : base(acquirerContext)
        {
        }

        protected abstract Task<TDomain> InstantiateDomain(TRequest3 request);

        protected virtual Task WriteOnRepository(TDomain domain, TRequest3 request)
        {
            return DbSet.AddAsync(domain).AsTask();
        }

        public Task<CommandResponse<TIdentifier>> Handle(TRequest3 request, CancellationToken cancellationToken)
        {
            return CustomHandler<TIdentifier, TDomain, TRequest3>(request, ChatContext, InstantiateDomain,
                WriteOnRepository);
        }
    }
}