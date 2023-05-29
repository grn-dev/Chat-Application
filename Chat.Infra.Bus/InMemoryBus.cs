using System.Threading.Tasks;
using FluentValidation.Results;
using Chat.Domain.Attributes;
using Chat.Domain.Core.Events;
using Chat.Domain.Core.SeedWork;
using MediatR;
using NetDevPack.Mediator;
using NetDevPack.Messaging;

namespace Chat.Infra.Bus
{
    [Bean]
    public sealed class InMemoryBus : IMyMediatorHandler
    {
        private readonly IMediator _mediator;
        private readonly IEventStore _eventStore;

        public InMemoryBus(IEventStore eventStore, IMediator mediator)
        {
            _eventStore = eventStore;
            _mediator = mediator;
        }

        public async Task PublishEvent<T>(T @event) where T : Event
        {
            if (!@event.MessageType.Equals("DomainNotification"))
                _eventStore?.Save(@event);

            await _mediator.Publish(@event);
        }

        public async Task<ValidationResult> SendCommand<T>(T command) where T : Command
        {
            return await _mediator.Send(command);
        }
        public async Task<TResponse> SendCommand<TResponse>(IRequest<TResponse> request)
        {
            return await _mediator.Send(request);
        }
        public async Task<TResponse> SendQuery<TResponse>(IRequest<TResponse> request)
        {
            return await _mediator.Send(request);
        }
    }
}
