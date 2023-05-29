using System;
using Chat.Domain.Models;

namespace Chat.Application.ViewModels
{
    public partial class ChannelViewModel
    {
        public class ChannelInfo
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

        public class MyChannel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public bool Closed { get; set; }
            public string Topic { get; set; }
        }

        public class ChannelExpose
        {
            public int Id { get; set; }
            public DateTime? LastNudged { get; set; }
            public string Name { get; set; }
            public bool IsPrivate { get; set; }
            public bool Closed { get; set; }
            public string Topic { get; set; }
            public string InviteCode { get; set; }
        }

        public class RegisterChannelOutput
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Topic { get; set; }
        }

        public class JoinChannel
        {
            public string Name { get; set; }
            public string InviteCode { get; set; }
        }

        public class LeaveChannel
        {
            public string Name { get; set; }
        }

        public class RegisterChannel
        {
            public bool IsPrivate { get; set; }
            public string Topic { get; set; }
            public string InviteCode { get; set; }
        }

        public class UpdateChannel : RegisterChannel
        {
            public int Id { get; set; }
        }
    }
}