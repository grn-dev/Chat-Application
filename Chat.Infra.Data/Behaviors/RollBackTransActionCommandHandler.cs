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
    public class RollBackTransActionCommandHandler : IRequestHandler<RollBackTransActionCommand>
    {
        private readonly ChatContext _dbContext;


        public RollBackTransActionCommandHandler(ChatContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Unit> Handle(RollBackTransActionCommand request, CancellationToken cancellationToken)
        {

            _dbContext.RollbackTransaction();

            return await Task.FromResult(Unit.Value);
        }
    }
}
