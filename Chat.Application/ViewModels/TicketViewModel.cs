using System;
using Chat.Domain.Models.Ticket;

namespace Chat.Application.ViewModels
{
    public partial class TicketViewModel
    {
        public class TicketExpose
        {
            public int Id { get; set; }
            public int GroupId { get; set; }
            public TicketStatus TicketStatus { get; set; }
            public string Subject { get; set; }
            public int? TicketCategoryId { get; set; }
            public TicketType? TicketType { get; set; }
            public Priority? Priority { get; set; }
        }

        public class RegisterTicketOutput
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class RegisterTicket
        {
            public string Subject { get; set; }
            public int? TicketCategoryId { get; set; }
            public TicketType? TicketType { get; set; }
            public Priority? Priority { get; set; }
        }

        public class UpdateTicket : RegisterTicket
        {
            public int Id { get; set; }
        }

        public class CloseTicket
        {
            public int Id { get; set; }
            //public TicketStatus TicketStatus { get; set; }
        }
        public class InprogressTicket
        {
            public int Id { get; set; }
            //public TicketStatus TicketStatus { get; set; }
        }

        public class JoinRoomCommandTicket
        {
            public Guid UserId { get; set; }
            public int ChatRoomId { get; set; }
            public int TicketId { get; set; }
            public string RoomName { get; set; }
        }

        public class JoinTicket
        {
            public string RoomName { get; set; }
        }

        public class TicketAdditionalDataExpose : TicketExpose
        {
            public ChatRoomViewModel.ChatRoomExpose ChatRoom { get; set; }
        }
    }
}