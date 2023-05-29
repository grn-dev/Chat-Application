using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using NetDevPack.Messaging;
using Chat.Domain.Common;
using Chat.Application.Interfaces;
using System;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using Chat.Domain.Core.SeedWork;
using Chat.Domain.Events;
using Chat.Infra.Data.Configurations.Applicator;
using Chat.Infra.Data.Logging;
using Chat.Domain.Models;
using Chat.Domain.Models.Ticket;
using Chat.Domain.Models.User;

namespace Chat.Infra.Data.Context
{
    public sealed partial class ChatContext : DbContext, IUnitOfWork
    {
        private readonly IMyMediatorHandler _mediatorHandler;
        private readonly ICurrentUserService _currentUserService;

        private readonly IPublishIntegrationEventService _publishIntegrationEventService;

        private readonly LogCollection _logCollection;

        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;
        public Guid GetCurrentTransactionId() => _currentTransactionId;
        public bool HasActiveTransaction => _currentTransaction != null;

        private IDbContextTransaction _currentTransaction;
        private Guid _currentTransactionId;

        public ChatContext(DbContextOptions<ChatContext> options, IMyMediatorHandler mediatorHandler,
            ICurrentUserService currentUserService, IPublishIntegrationEventService publishIntegrationEventService,
            LogCollection logCollection) : base(options)
        {
            _mediatorHandler = mediatorHandler;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = true;
            _currentUserService = currentUserService;
            _publishIntegrationEventService = publishIntegrationEventService;
            _logCollection = logCollection;
            //_IntegrationEventService = integrationEventService;
        }

        #region dataSet

        public DbSet<Chat.Domain.Models.Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<ApplicationSetting> ApplicationSettings { get; set; }
        public DbSet<TicketCategory> TicketCategories { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<ChatRoomUser> ChatRoomUsers { get; set; }
        public DbSet<ChatRoomMessage> ChatRoomMessages { get; set; }

        public DbSet<Direct> Directs { get; set; }
        public DbSet<DirectUser> DirectUsers { get; set; }
        public DbSet<DirectMessage> DirectMessages { get; set; }

        #endregion


        public IUnitOfWork UnitOfWork => this;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.HasSequence<int>("ReferenceNumber");
            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.Ignore<Event>();

            modelBuilder.ApplyAllConfigurations();
            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> Commit()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                var methodTypeArg = entry.Entity.GetType()?.BaseType.GetGenericArguments().FirstOrDefault();
                if (methodTypeArg != null)
                    if (entry.Entity.GetType().BaseType == (typeof(BaseModel<>).MakeGenericType(methodTypeArg)))
                    {
                        switch (entry.State)
                        {
                            case EntityState.Added:
                                (entry.Entity).GetType().GetProperty(nameof(BaseModel<int>.CreatorUserId))
                                    .SetValue(entry.Entity, _currentUserService.UserId ?? Guid.Empty);
                                break;

                            //case EntityState.Modified:
                            //    (entry.Entity).GetType().GetProperty(nameof(BaseModel<int>.ModifyDate)).SetValue(entry.Entity, DateTime.Now);
                            //    break;
                        }
                    }
            }


            // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
            // performed through the DbContext will be committed

            var success = await SaveChangesAsync() >= 0;
            // var success = await SaveChangesAsync() > 0;

            // Dispatch Domain Events collection. 
            // Choices:
            // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
            // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
            // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
            // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
            await _mediatorHandler.PublishDomainEvents(this).ConfigureAwait(false);

            return success;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null) return null;

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            _currentTransactionId = _currentTransaction.TransactionId;
            return _currentTransaction;
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (_currentTransaction == null)
                return;

            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != _currentTransaction)
                throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try
            {
                await SaveChangesAsync();
                transaction.Commit();
                await _publishIntegrationEventService.PublishEventsThroughEventBusAsync(_currentTransaction
                    .TransactionId);
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
                _logCollection.RemoveLog(LogCollection.ContextEventLogName);
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public override void Dispose()
        {
            if (_currentTransaction != null)
                CommitTransactionAsync(_currentTransaction).ConfigureAwait(false).GetAwaiter().GetResult();

            base.Dispose();
        }

        public override async ValueTask DisposeAsync()
        {
            if (_currentTransaction != null)
                await CommitTransactionAsync(_currentTransaction);

            await base.DisposeAsync();
            return;
        }
    }

    public static class MediatorExtension
    {
        public static async Task PublishDomainEvents<T>(this IMyMediatorHandler mediator, T ctx) where T : DbContext
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<MyEntity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
            {
                await mediator.PublishEvent(domainEvent);
            }
            // var tasks = domainEvents
            //     .Select(async (domainEvent) => { await mediator.PublishEvent(domainEvent); });
            //
            // await Task.WhenAll(tasks);
        }
    }
}