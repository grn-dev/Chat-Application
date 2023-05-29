using System.Threading;
using System.Threading.Tasks;
using Chat.Application.Configuration.Data.BasicQuery;
using Chat.Application.Interfaces;
using Chat.Domain.Attributes;
using Chat.Domain.Commands.ChatRoom;
using Chat.Domain.Core.SeedWork;
using Chat.Domain.Models;
using Chat.Domain.Models.Events;
using Chat.Domain.Models.User;
using MediatR;
using static Chat.Application.ViewModels.UserViewModel;

namespace Chat.Application.Events
{
    [Bean]
    public class ChatRoomEventHandler :
        INotificationHandler<ChatRoomRegisteredEvent>
    {
        private readonly IMyMediatorHandler _mediator;
        private readonly ICurrentUserService _currentUserService;

        public ChatRoomEventHandler(IMyMediatorHandler mediatorHandler, ICurrentUserService currentUserService)
        {
            _mediator = mediatorHandler;
            _currentUserService = currentUserService;
        }

        public async Task Handle(ChatRoomRegisteredEvent notification, CancellationToken cancellationToken)
        {
            var user = await _mediator.SendQuery(
                new GetPredicateToDestQuery<User, int, UserExpose>()
                {
                    Predicate = c => c.UserId == _currentUserService.UserId.Value
                });

            if (notification.Type == ChatRoomType.TICKET)
                await RegisterChatRoomUser(notification, user.Id, false);
            else
                await RegisterChatRoomUser(notification, user.Id);
        }

        private async Task RegisterChatRoomUser(ChatRoomRegisteredEvent notification, int userId, bool isAdmin = true)
        {
            await _mediator.SendCommand(new RegisterChatRoomUserCommand()
            {
                UserId = userId,
                ChatRoomId = notification.IdAccessor(),
                Status = ChatRoomUserStatus.DEFAULT,
                IsAdmin = isAdmin
            });
        }
    }
}