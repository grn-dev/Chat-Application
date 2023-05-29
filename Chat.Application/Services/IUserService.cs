using System;
using System.Threading.Tasks;
using Chat.Application.ViewModels;
using FluentValidation.Results;
using Garnet.Standard.Pagination;

namespace Chat.Application.Services
{
    public interface IUserService
    {
        Task<IPagedElements<UserViewModel.UserExpose>> GetAllPagination(IPagination pagination);
        Task<UserViewModel.UserExpose> Get(int id);  
        Task<UserViewModel.UserBaseExpose> Get(Guid userId);  
        Task<ValidationResult> Register(UserViewModel.RegisterUser user);
        Task<ValidationResult> Update(UserViewModel.UpdateUser user);  
        //Task<ValidationResult> Delete(int id);
        Task UpdateActivity(string userId);
    }
}



