using System.Threading.Tasks;
using Chat.Application.ViewModels;

namespace Chat.Application.Services
{
    public interface IHubOutsideService
    {
        Task SendNoticeLefRoom(MessageViewModel.SendNoticeToRoom noticeRoom);
        Task SendNoticeJoinToRoom(MessageViewModel.SendNoticeToRoom noticeRoom);
        Task SendMessageToRoom(MessageViewModel.MessageChatRoom messageChatRoom, string roomName);

        Task SendMessageToDirect(MessageViewModel.MessageDirect messageDirect, string directName);
        Task AddDirectUserIntoDirect(MessageViewModel.AddDirectUserIntoDirect directUserIntoDirect);
    }
}