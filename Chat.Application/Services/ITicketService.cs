using System;
using System.Threading.Tasks;
using Chat.Application.ViewModels;
using Chat.Domain.Core.SeedWork;
using FluentValidation.Results;
using Garnet.Standard.Pagination;

namespace Chat.Application.Services
{
    public interface ITicketService
    {
        Task<CommandResponse<TicketViewModel.RegisterTicketOutput>> Register(TicketViewModel.RegisterTicket ticket);
        Task<IPagedElements<TicketViewModel.TicketExpose>> GetAllPagination(IPagination pagination);
        Task<TicketViewModel.TicketAdditionalDataExpose> Get(int id);
        Task<ValidationResult> JoinTicket(TicketViewModel.JoinTicket chatRoomJoinRoom, Guid userId);
        Task<ValidationResult> CloseTicket(TicketViewModel.CloseTicket closeTicket);
        Task<ValidationResult> InprogressTicket(TicketViewModel.InprogressTicket closeTicket);
        Task<IPagedElements<MessageViewModel.MessageChatRoom>> GetMessagesGroup(IPagination pagination,
            string name, Guid currentUserId);
    }
}