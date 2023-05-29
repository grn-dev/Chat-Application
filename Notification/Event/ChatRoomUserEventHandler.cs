using System.Threading;
using System.Threading.Tasks;
using Chat.Application.Configuration.Data.BasicQuery;
using Chat.Application.Services;
using Chat.Application.ViewModels;
using Chat.Domain.Attributes;
using Chat.Domain.Core.SeedWork;
using Chat.Domain.Models;
using Chat.Domain.Models.Events;
using Chat.Domain.Models.User;
using MediatR;

namespace Chat.Event
{
    [Bean]
    public class ChatRoomUserEventHandler :
        INotificationHandler<ChatRoomUserRegisteredEvent>,
        INotificationHandler<ChatRoomUserChangedStatusEvent>
    {
        private readonly IMyMediatorHandler _mediator;
        private readonly IHubOutsideService _hubOutsideService;

        public ChatRoomUserEventHandler(IMyMediatorHandler mediator, IHubOutsideService hubOutsideService)
        {
            _mediator = mediator;
            _hubOutsideService = hubOutsideService;
        }

        public async Task Handle(ChatRoomUserRegisteredEvent notification, CancellationToken cancellationToken)
        {
            var chatRoom = await _mediator.SendQuery(
                new GetPredicateToDestQuery<ChatRoom, int, ChatRoomViewModel.ChatRoomBaseExpose>()
                {
                    Predicate = c => c.Id == notification.ChatRoomId
                });

            var user = await _mediator.SendQuery(
                new GetPredicateToDestQuery<User, int, UserViewModel.UserBaseExpose>()
                {
                    Predicate = c => c.Id == notification.UserId
                });

            await _hubOutsideService.SendNoticeJoinToRoom(new MessageViewModel.SendNoticeToRoom()
            {
                Username = user.UserName,
                RoomName = chatRoom.Name,
                UserId = user.UserId,
            });
        }

        public async Task Handle(ChatRoomUserChangedStatusEvent notification, CancellationToken cancellationToken)
        {
            var chatRoom = await _mediator.SendQuery(
                new GetPredicateToDestQuery<ChatRoom, int, ChatRoomViewModel.ChatRoomBaseExpose>()
                {
                    Predicate = c => c.Id == notification.ChatRoomId
                });

            var user = await _mediator.SendQuery(
                new GetPredicateToDestQuery<User, int, UserViewModel.UserBaseExpose>()
                {
                    Predicate = c => c.Id == notification.UserId
                });


            if (notification.Status == ChatRoomUserStatus.LEFT)
            {
                await _hubOutsideService.SendNoticeLefRoom(new MessageViewModel.SendNoticeToRoom()
                {
                    Username = user.UserName,
                    RoomName = chatRoom.Name,
                    UserId = user.UserId
                });
            }
        }
    }
}