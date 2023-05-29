using System;
using Chat.Domain.Models;

namespace Chat.Application.ViewModels
{
    public partial class ChatRoomViewModel
    {
        public class RegisterChatRoomOutput
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Topic { get; set; }
        }

        public class ChatRoomExpose
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public bool Closed { get; set; }
            public string Topic { get; set; }
            public string InviteCode { get; set; }
            public bool IsPrivate { get; set; }
            public DateTime? LastNudged { get; set; }
            public ChatRoomType Type { get; set; }
        }

        public class HubJoinRoomClientToServer
        {
            public string RoomName { get; set; }
            public string InviteCode { get; set; }
        }

        public class HubJoinRoomServerToClient
        {
            public string Content { get; set; }
        }

        public class JoinRoomRegister
        {
            public Guid UserId { get; set; }
            public string RoomName { get; set; }
            public string InviteCode { get; set; }
        }

        public class HubCreatRoomClientToServer
        {
            public string InviteCode { get; set; }
            public string Topic { get; set; }
            public bool IsPrivate { get; set; }
        }

        public class HubCreatRoomServerToClient
        {
            public string RoomName { get; set; }
            public string UserName { get; set; }
        }

        public class CreatRoomRegister
        {
            public Guid UserId { get; set; }
            public string InviteCode { get; set; }
            public string Topic { get; set; }
            public bool IsPrivate { get; set; }
        }

        public class RegisterChatRoom
        {
            public string Topic { get; set; }
            public string InviteCode { get; set; }
            public bool IsPrivate { get; set; }
        }

        public class LeaveChatRoom
        {
            public Guid UserId { get; set; }
            public string Name { get; set; }
        }

        public class JoinChatRoom
        {
            public Guid UserId { get; set; }
            public string Name { get; set; }
            public string InviteCode { get; set; }
        }

        public class ChatRoomBaseExpose
        {
            public int Id { get; set; }
            public DateTime? LastNudged { get; set; }
            public string Name { get; set; }
        }
    }
}