using Chat.Domain.Attributes;
using Chat.Domain.Models;
using Chat.Domain.Core.SeedWork;
using System.Threading.Tasks;
using Chat.Application.Configuration.Data.BasicQuery;

namespace Chat.Application.DomainService
{
    [Bean]
    public class ChatRoomUserRuleChecker : IChatRoomUserRuleChecker
    {
        private readonly IMyMediatorHandler _mediator;
        public ChatRoomUserRuleChecker(IMyMediatorHandler mediator)
        {
            _mediator = mediator;
        }

        public async Task<bool> ChatRoomUserMustBeUnique(int userId, int roomId)
        {
            return await _mediator.SendQuery(new AnyByPredicateQuery<ChatRoomUser, int>()
            {
                Predicate = c => c.UserId == userId & c.ChatRoomId == roomId
            });
        }


    }
}

