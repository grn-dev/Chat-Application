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
    public class ChatRoomMessageEventHandler :
        INotificationHandler<ChatRoomMessageRegisteredEvent>
    {
        private readonly IMyMediatorHandler _mediator;
        private readonly IHubOutsideService _hubOutsideService;

        public ChatRoomMessageEventHandler(IMyMediatorHandler mediatorHandler, IHubOutsideService hubOutsideService)
        {
            _mediator = mediatorHandler;
            _hubOutsideService = hubOutsideService;
        }

        public async Task Handle(ChatRoomMessageRegisteredEvent notification, CancellationToken cancellationToken)
        {
            await SendMessageToRoome(notification);

            await UpdateLastNudgedChatRoom(notification);
        }

        private async Task UpdateLastNudgedChatRoom(ChatRoomMessageRegisteredEvent notification)
        {
            await _mediator.SendCommand(new UpdateLastNudgedCommand()
            {
                Id = notification.ChatRoomId
            });
        }

        private async Task SendMessageToRoome(ChatRoomMessageRegisteredEvent notification)
        {
            //TODO GET ALLL IFO by one query
            var room = await _mediator.SendQuery(
                new GetPredicateToDestQuery<ChatRoom, int, ChatRoomViewModel.ChatRoomBaseExpose>()
                {
                    Predicate = c => c.Id == notification.ChatRoomId
                });

            var message = await _mediator.SendQuery(
                new GetPredicateToDestQuery<ChatRoomMessage, int, MessageViewModel.MessageChatRoom>()
                {
                    Predicate = c => c.Id == notification.ChatRoomMessageId()
                });

            await _hubOutsideService.SendMessageToRoom(message, room.Name);
        }
    }
}