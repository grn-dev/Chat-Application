using Chat.Domain.Attributes;
using Chat.Domain.Commands;
using Chat.Infra.Data.Context;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chat.Infra.Data.Behaviors
{
    [Bean]
    public class CommitTransActionCommandHandler : IRequestHandler<CommitTransActionCommand>
    {
        private readonly ChatContext _dbContext;


        public CommitTransActionCommandHandler(ChatContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Unit> Handle(CommitTransActionCommand request, CancellationToken cancellationToken)
        {

            await _dbContext.CommitTransactionAsync(_dbContext.GetCurrentTransaction());

            return await Task.FromResult(Unit.Value);
        }
    }
}
