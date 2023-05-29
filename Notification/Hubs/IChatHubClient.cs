using System.Threading.Tasks;
using static Chat.Application.ViewModels.MessageViewModel;

namespace Chat.Hubs
{
    public interface IChatHubClient
    {
        Task ReceiveMessageDirect(MessageDirect message);
        Task ReceiveMessageChatRoom(MessageChatRoom message);
        Task ReceiveNoticeChatRoom(ReceiveNoticeChatRoom noticeChatRoom);
        Task OtherUserIsTyping(OtherUserIsTyping isTyping);
    }
}