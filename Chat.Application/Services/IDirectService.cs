using System;
using System.Threading.Tasks;
using Chat.Application.ViewModels;
using Chat.Domain.Core.SeedWork;
using Garnet.Standard.Pagination;

namespace Chat.Application.Services
{
    public interface IDirectService
    {
        Task<IPagedElements<DirectViewModel.DirectExpose>> GetAllPagination(IPagination pagination);

        Task<IPagedElements<MessageViewModel.MessageDirect>> GetMessagesDirect(IPagination pagination, string name);

        Task<DirectViewModel.DirectExpose> Get(int id);

        Task<CommandResponseBatchValidationResult<DirectViewModel.RegisterDirectOutput>> Register(DirectViewModel.RegisterDirect direct,Guid currentUserId);

        // Task<ValidationResult> Update(DirectViewModel.UpdateDirect direct);
        Task<IPagedElements<DirectViewModel.MyDirect>> GetMyDirect(IPagination pagination, Guid currentUserId);
    }
}