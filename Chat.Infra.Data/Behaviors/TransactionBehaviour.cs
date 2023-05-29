using Chat.Domain.Attributes;
using Chat.Infra.Data.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Chat.Domain.Commands.Behaviors
{
    public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ChatContext _dbContext;


        public TransactionBehaviour(ChatContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var response = default(TResponse);
            try
            {
                if (!IsTransactional(next.Target.GetType()))
                {
                    return await next();
                }
                if (_dbContext.HasActiveTransaction)
                {
                    return await next();
                }

                var strategy = _dbContext.Database.CreateExecutionStrategy();

                await strategy.ExecuteAsync(async () =>
                {
                    Guid transactionId;

                    var transaction = await _dbContext.BeginTransactionAsync();

                    //Begin transaction

                    response = await next();

                    // Commit transaction automatically run by DbContext's Dispose
                    transactionId = transaction.TransactionId;
                });

                return response;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "ERROR Handling transaction for {CommandName} ({@Command})", typeName, request);
                _dbContext.RollbackTransaction();
                throw;
            }
        }


        private bool IsTransactional(Type request)
        {
            return request.GetGenericArguments().ToList().Any(c => c.GetCustomAttributes(typeof(TransactionalAttribute), true).Length > 0);
        }
    }
}
