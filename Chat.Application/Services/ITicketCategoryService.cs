using FluentValidation.Results;
using System.Threading.Tasks; 
using Chat.Application.ViewModels;
using Garnet.Standard.Pagination;

namespace Chat.Application.Services.TicketCategory
{
    public interface ITicketCategoryService
    {
        Task<IPagedElements<TicketCategoryViewModel.TicketCategoryExpose>> GetAllPagination(IPagination pagination);
        Task<TicketCategoryViewModel.TicketCategoryExpose> Get(int id);  
        Task<ValidationResult> Register(TicketCategoryViewModel.RegisterTicketCategory ticketCategory);
        Task<ValidationResult> Update(TicketCategoryViewModel.UpdateTicketCategory ticketCategory);  
        Task<ValidationResult> Delete(int id); 
    }
}



