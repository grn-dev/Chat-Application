using System.Threading.Tasks;
using Chat.Application.ViewModels;
using FluentValidation.Results;
using Garnet.Standard.Pagination;

namespace Chat.Application.Services
{
    public interface IAttachmentService
    {
        Task<IPagedElements<AttachmentViewModel.AttachmentExpose>> GetAllPagination(IPagination pagination);
        Task<AttachmentViewModel.AttachmentExpose> Get(int id);
        Task<ValidationResult> RegisterToDirect(AttachmentViewModel.RegisterAttachmentToDirect attachment);
        Task<ValidationResult> RegisterToRoom(AttachmentViewModel.RegisterAttachmentToRoom attachment);
    }
}