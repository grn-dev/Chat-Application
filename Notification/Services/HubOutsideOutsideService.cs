using System;
using System.Threading.Tasks;
using AutoMapper;
using Chat.Application.Services;
using Chat.Application.ViewModels;
using Chat.Domain.Attributes;
using Chat.Enums;
using Chat.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Services
{
    [Bean]
    public class HubOutsideOutsideService : IHubOutsideService
    {
        private readonly IMapper _mapper;
        private readonly IHubContext<ChatHub, IChatHubClient> _hubContext;
        private readonly ICache _cache;

        public HubOutsideOutsideService(IHubContext<ChatHub, IChatHubClient> hubContext, IMapper mapper, ICache cache)
        {
            _hubContext = hubContext;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task SendNoticeLefRoom(MessageViewModel.SendNoticeToRoom noticeRoom)
        {
            var infoHubUser = await _cache.Get<UserViewModel.UserBaseInfoHub>(noticeRoom.UserId.ToString());


            await _hubContext.Clients.Group(noticeRoom.RoomName).ReceiveNoticeChatRoom(
                new MessageViewModel.ReceiveNoticeChatRoom()
                {
                    Content = UserLeftChatRoom.REGISTER_Body(noticeRoom.Username).Desc,
                });

            await _hubContext.Groups.RemoveFromGroupAsync(infoHubUser.ConnectionId, noticeRoom.RoomName);
        }

        public async Task SendNoticeJoinToRoom(MessageViewModel.SendNoticeToRoom noticeRoom)
        {
            var infoHubUser = await _cache.Get<UserViewModel.UserBaseInfoHub>(noticeRoom.UserId.ToString());

            await _hubContext.Groups.AddToGroupAsync(infoHubUser.ConnectionId, noticeRoom.RoomName);

            await _hubContext.Clients.Group(noticeRoom.RoomName).ReceiveNoticeChatRoom(
                new MessageViewModel.ReceiveNoticeChatRoom()
                {
                    Content = UserJoinedChatRoom.REGISTER_Body(noticeRoom.Username).Desc,
                });
        }


        public async Task SendMessageToRoom(MessageViewModel.MessageChatRoom messageChatRoom, string roomName)
        {
            await _hubContext.Clients.Group(roomName).ReceiveMessageChatRoom(messageChatRoom);
        }


        public async Task SendMessageToDirect(MessageViewModel.MessageDirect messageDirect, string directName)
        {
            await _hubContext.Clients.Group(directName).ReceiveMessageDirect(messageDirect);
        }

        public async Task AddDirectUserIntoDirect(MessageViewModel.AddDirectUserIntoDirect directUserIntoDirect)
        {
            foreach (var userId in directUserIntoDirect.UserIds)
            {
                var user = await _cache.Get<UserViewModel.UserBaseInfoHub>(userId.ToString());
                if (user != null)
                    await _hubContext.Groups.AddToGroupAsync(user.ConnectionId, directUserIntoDirect.DirectName);
            }
        }
    }
}