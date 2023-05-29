using MediatR;
using NetDevPack.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Core.SeedWork
{
    public interface IMyMediatorHandler : IMediatorHandler
    {
        Task<TResponse> SendQuery<TResponse>(IRequest<TResponse> request);
        Task<TResponse> SendCommand<TResponse>(IRequest<TResponse> request);
    }
}
