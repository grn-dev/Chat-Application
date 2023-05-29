using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Chat.Application.Configuration.Data.BasicQuery;
using Chat.Application.Services;
using Chat.Application.ViewModels;
using Chat.Domain.Attributes;
using Chat.Domain.Core.SeedWork;
using Chat.Domain.Models;
using Chat.Domain.Models.Events;
using MediatR;

namespace Chat.Event
{
    [Bean]
    public class DirectEventHandler :
        INotificationHandler<DirectRegisteredEvent>
    {
        private readonly IMyMediatorHandler _mediator;
        private readonly IHubOutsideService _hubOutsideService;

        public DirectEventHandler(IMyMediatorHandler mediatorHandler, IHubOutsideService hubOutsideService)
        {
            _mediator = mediatorHandler;
            _hubOutsideService = hubOutsideService;
        }

        public async Task Handle(DirectRegisteredEvent notification, CancellationToken cancellationToken)
        {
            var users = await _mediator.SendQuery(
                new GetAllByPredicateToDestQuery<DirectUser, int, DirectViewModel.DirectUserExpose>()
                {
                    Predicate = c => c.Direct.Name == notification.DirectName
                });

            //TODO check
            await _hubOutsideService.AddDirectUserIntoDirect(new MessageViewModel.AddDirectUserIntoDirect()
            {
                DirectName = notification.DirectName,
                UserIds = users.Select(x => x.User.UserId).ToList()
            });
        }
    }
}