using System.Linq;
using System.Threading.Tasks;
using Chat.Application.Configuration.Data.BasicQuery;
using Chat.Domain.Attributes;
using Chat.Domain.Models;
using Chat.Domain.Core.SeedWork;

namespace Chat.Application.DomainService
{
    [Bean]
    public class ChatRoomMessageRuleChecker : IChatRoomMessageRuleChecker
    {
        private readonly IMyMediatorHandler _mediator;

        public ChatRoomMessageRuleChecker(IMyMediatorHandler mediator)
        {
            _mediator = mediator;
        }


        public async Task<bool> ChatRoomMessageAdminMustbeSendMessage(int chatRoomId, int senderId)
        {
            return await _mediator.SendQuery(new AnyByPredicateQuery<ChatRoom, int>()
            {
                Predicate = c => c.ChatRoomUsers.Any(x => x.IsAdmin == false && x.UserId == senderId)
                                 && c.Type == ChatRoomType.CHANNEL
                                 && c.Id == chatRoomId
            });
        }
    }
}