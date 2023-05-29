using Chat.Domain.Models;
using System;
using System.Collections.Generic;
using static Chat.Application.ViewModels.UserViewModel;

namespace Chat.Application.ViewModels
{
    public partial class MessageViewModel
    {
        public class MessageDirect
        {
            public string Content { get; set; }
            public DateTime CreateDate { get; set; }
            public string UserName { get; set; }
            public int? ParentId { get; set; }
            public int MessageId { get; set; }
            public AttachmentViewModel.AttachmentBaseExpose Attachment { get; set; }
        }

        public class MessageChatRoom
        {
            public string Content { get; set; }
            public DateTime CreateDate { get; set; }
            public string UserName { get; set; }
            public int? ParentId { get; set; }
            public int MessageId { get; set; }
            public AttachmentViewModel.AttachmentBaseExpose Attachment { get; set; }
        }

        public class ReceiveNoticeChatRoom
        {
            public string Content { get; set; }
        }

        public class AddDirectUserIntoDirect
        {
            public string DirectName { get; set; }
            public List<Guid> UserIds { get; set; }
        }

        public class SendNoticeToRoom
        {
            public string RoomName { get; set; }
            public Guid UserId { get; set; }
            public string Username { get; set; }
        }

        // public class SendMessageToRoom
        // {
        //     public string RoomName { get; set; }
        //     public string Content { get; set; }
        //     public string Username { get; set; }
        //     public int MessageId { get; set; }
        //     public AttachmentViewModel.AttachmentBaseExpose Attachment { get; set; }
        // }

        public class RegisterMessage
        {
            public string Content { get; set; }
            public int UserId { get; set; }
            public int? ParentId { get; set; }
            public int? AttachmentId { get; set; }
            public MessageType Type { get; set; }
        }

        public class RegisterMessageToRoom
        {
            public string RoomName { get; set; }
            public string Content { get; set; }
            public int? ParentId { get; set; }
            public int? AttachmentId { get; set; }
        }

        public class RegisterMessageToRoomAttachment
        {
            public string RoomName { get; set; }
            public string Content { get; set; }
            public int? ParentId { get; set; }
        }


        public class OtherUserIsTyping
        {
            public string UserName { get; set; }
        }

        public class UserTyping
        {
            public string GroupName { get; set; }
        }

        public class RegisterMessageToDirect
        {
            public string Content { get; set; }
            public int? ParentId { get; set; }
            public int? AttachmentId { get; set; }
            public string DirectName { get; set; }
        }

        public class RegisterMessageToDirectAttachment
        {
            public string Content { get; set; }
            public int? ParentId { get; set; }
            public string DirectName { get; set; }
        }


        public class MessageIncludeUserRoom
        {
            public int Id { get; set; }
            public string Content { get; set; }
            public DateTimeOffset When { get; set; }
            public ChatRoomViewModel.ChatRoomBaseExpose ChatRoom { get; set; }
            public UserBaseExpose User { get; set; }
        }

        public class MessageExpose
        {
            public int Id { get; set; }
            public string Content { get; set; }
            public DateTimeOffset When { get; set; }
            public bool HtmlEncoded { get; set; }
            public MessageType Type { get; set; }
            public string HtmlContent { get; set; }
            public int? RoomId { get; set; }
            public UserBaseExpose User { get; set; }
            public string ImageUrl { get; set; }
            public string Source { get; set; }
        }

        public class UpdateMessage
        {
            public int Id { get; set; }
        }
    }
}