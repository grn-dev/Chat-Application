using System.Threading;
using System.Threading.Tasks;
using Chat.Application.Configuration.Data.BasicQuery;
using Chat.Application.Services;
using Chat.Application.ViewModels;
using Chat.Domain.Attributes;
using Chat.Domain.Commands.ChatRoomUser;
using Chat.Domain.Core.SeedWork;
using Chat.Domain.Models;
using Chat.Domain.Models.Events;
using MediatR;

namespace Chat.Event
{
    [Bean]
    public class DirectMessageEventHandler :
        INotificationHandler<DirectMessageRegisteredEvent>
    {
        private readonly IMyMediatorHandler _mediator;
        private readonly IHubOutsideService _hubOutsideService;

        public DirectMessageEventHandler(IMyMediatorHandler mediatorHandler, IHubOutsideService hubOutsideService)
        {
            _mediator = mediatorHandler;
            _hubOutsideService = hubOutsideService;
        }

        private async Task UpdateLastNudgedChatRoom(ChatRoomMessageRegisteredEvent notification)
        {
            await _mediator.SendCommand(new UpdateLastNudgedCommand()
            {
                Id = notification.ChatRoomId
            });
        }

        private async Task SendMessageToDirect(DirectMessageRegisteredEvent notification)
        {
            var direct = await _mediator.SendQuery(
                new GetPredicateToDestQuery<Direct, int, DirectViewModel.DirectExpose>()
                {
                    Predicate = c => c.Id == notification.DirectId
                });

            var message = await _mediator.SendQuery(
                new GetPredicateToDestQuery<DirectMessage, int, MessageViewModel.MessageDirect>()
                {
                    Predicate = c => c.Id == notification.DirectMessageId()
                });

            await _hubOutsideService.SendMessageToDirect(message, direct.Name);
        }

        public async Task Handle(DirectMessageRegisteredEvent notification, CancellationToken cancellationToken)
        {
            await SendMessageToDirect(notification); 
        }
    }
}