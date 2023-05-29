using System;
using Chat.Domain.Models;

namespace Chat.Application.ViewModels
{
    public partial class GroupViewModel
    {
        public class GroupInfo
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

        public class GroupExpose
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

        public class MyGroup
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public bool Closed { get; set; }
            public string Topic { get; set; }
        }

        public class RegisterGroupOutput
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Topic { get; set; }
        }

        public class RegisterGroup
        {
            public bool IsPrivate { get; set; }
            public string Topic { get; set; }
            public string InviteCode { get; set; }
        }
        public class UpdateGroup : RegisterGroup
        {
            public int Id { get; set; }
        }

        public class JoinGroup
        {
            public string Name { get; set; }
            public string InviteCode { get; set; }
        }

        public class LeaveGroup
        {
            public string Name { get; set; }
        }
    }
}