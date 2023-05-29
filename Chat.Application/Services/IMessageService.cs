using System;
using System.Threading.Tasks;
using Chat.Application.ViewModels;
using Chat.Domain.Core.SeedWork;
using FluentValidation.Results;
using Garnet.Standard.Pagination;

namespace Chat.Application.Services
{
    public interface IMessageService
    {
        Task<IPagedElements<MessageViewModel.MessageExpose>> GetAllPagination(IPagination pagination);

        Task<MessageViewModel.MessageExpose> Get(int id);

        //Task<ValidationResult> Register(MessageViewModel.AddMessageRegisterToRoom message);
        Task<ValidationResult> Update(MessageViewModel.UpdateMessage message);

        Task<ValidationResult> RegisterMessageToRoom(MessageViewModel.RegisterMessageToRoom room, Guid userId);
        Task<ValidationResult> RegisterMessageToDirect(MessageViewModel.RegisterMessageToDirect direct, Guid userId);
    }
}