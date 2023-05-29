using System;
using System.Threading.Tasks;
using Chat.Application.Services;
using Chat.Application.ViewModels;
using Chat.Domain.Attributes;
using Chat.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Hubs
{
    [Bean]
    [Authorize()]
    public class ChatHub : Hub<IChatHubClient>

    {
        private static readonly TimeSpan DisconnectThreshold = TimeSpan.FromSeconds(10);

        private readonly Lazy<IChatRoomService> _chatService;
        private readonly Lazy<IUserService> _userService;
        private readonly Lazy<IMessageService> _messageService;
        private readonly ICache _cache;

        public ChatHub(Lazy<IChatRoomService> chatService, ICache cache, Lazy<IUserService> userService,
            Lazy<IMessageService> messageService)
        {
            _chatService = chatService;
            _cache = cache;
            _userService = userService;
            _messageService = messageService;
        }


        public override async Task OnConnectedAsync()
        {
            await SetUserInfo();
            await ConnectToOldChatRoomAndDirect();
            await base.OnConnectedAsync();
        }

        private async Task SetUserInfo()
        {
            var user = await _userService.Value.Get(new Guid(Context.UserIdentifier));

            await _cache.Set(Context.UserIdentifier, new UserViewModel.UserBaseInfoHub()
            {
                ConnectionId = Context.ConnectionId,
                UserId = new Guid(Context.UserIdentifier),
                UserName = user.UserName
            });
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await _userService.Value.UpdateActivity(Context.UserIdentifier);
            //RemoveFromGroupAsync
            await _cache.Remove(Context.UserIdentifier);
            await base.OnDisconnectedAsync(exception);
        }

        private async Task ConnectToOldChatRoomAndDirect()
        {
            foreach (var roomName in await _chatService.Value.GetRoomAndDirectNames(Context.User.GetUserId()))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
            }
        }

        public async Task SendMessageToRoom(MessageViewModel.RegisterMessageToRoom room)
        {
            await _messageService.Value.RegisterMessageToRoom(room, Context.User.GetUserId());
        }

        public async Task SendMessageToDirect(MessageViewModel.RegisterMessageToDirect direct)
        {
            await _messageService.Value.RegisterMessageToDirect(direct, Context.User.GetUserId());
        }

        public async Task UserTyping(MessageViewModel.UserTyping typing)
        {
            var user = await GetUserInfo();

            // await Clients.Group(typing.GroupName).OtherUserIsTyping(new MessageViewModel.OtherUserIsTyping()
            // {
            //     UserName = user.UserName
            // });
            await Clients.GroupExcept(typing.GroupName, user.ConnectionId).OtherUserIsTyping(
                new MessageViewModel.OtherUserIsTyping()
                {
                    UserName = user.UserName
                });
        }

        private async Task<UserViewModel.UserBaseInfoHub> GetUserInfo()
        {
            var userId = Context.User.GetUserId();
            var user = await _cache.Get<UserViewModel.UserBaseInfoHub>(userId.ToString());
            return user;
        }
    }
}