using Chat.Domain.Attributes;
using Chat.Domain.Models;
using Chat.Domain.Core.SeedWork;
using System.Threading.Tasks;
using Chat.Application.Configuration.Data.BasicQuery; 

namespace Chat.Application.DomainService
{
    [Bean]
    public class ChatRoomRuleChecker : IChatRoomRuleChecker
    {
        private readonly IMyMediatorHandler _mediator;
        public ChatRoomRuleChecker(IMyMediatorHandler mediator)
        {
            _mediator = mediator;
        }

        public async Task<bool> ChatRoomNameMustBeUnique(string roomName)
        {
            return await _mediator.SendQuery(new AnyByPredicateQuery<ChatRoom, int>()
            {
                Predicate = c => c.Name == roomName
            });
        }

    }
}

