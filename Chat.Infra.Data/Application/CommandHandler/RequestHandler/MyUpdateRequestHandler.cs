using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Domain;
using Chat.Application.Configuration.Exceptions;
using Chat.Domain.Commands;
using Chat.Domain.Common;
using Chat.Domain.Core.SeedWork;
using Chat.Infra.Data.Context;

namespace Chat.Infra.Data.Application.QueryHandler.RequestHandler
{
    public abstract class
        MyUpdateRequestHandler<TRequest, TDomain, TIdentifier>
        : NetDevPack.Messaging.CommandHandler, IRequestHandler<TRequest, CommandResponse<TIdentifier>>
        where TRequest : BaseCommand<TIdentifier>
        where TDomain : BaseModel<TIdentifier>, IAggregateRoot
    {
        protected readonly ChatContext _chatContext;
        protected readonly DbSet<TDomain> DbSet;

        protected MyUpdateRequestHandler(ChatContext chatContext)
        {
            _chatContext = chatContext;
            DbSet = chatContext.Set<TDomain>();
        }

        public virtual async Task<CommandResponse<TIdentifier>> Handle(TRequest request,
            CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return new CommandResponse<TIdentifier> { ValidationResult = request.ValidationResult };

            var domain = await InitialDomain(request); //await Repository.GetAsync(request.Id);
            if (domain is null)
            {
                throw new MyApplicationException(ApplicationErrorCode.ENTITY_NOT_FOUND);
            }
            var dbEntry = _chatContext.Entry(domain);
            dbEntry.State = EntityState.Unchanged;

            await UpdateFields(domain, request);

            return new CommandResponse<TIdentifier>()
            {
                ValidationResult = await Commit(_chatContext.UnitOfWork),
                Response = domain.Id
            };
        }


        protected virtual async Task<TDomain> InitialDomain(TRequest request)
        {
            return await DbSet.FindAsync(request.Id);
        }
        protected abstract Task UpdateFields(TDomain domain, TRequest request);
    }
}