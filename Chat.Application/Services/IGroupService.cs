using System;
using System.Threading.Tasks;
using Chat.Application.ViewModels;
using Chat.Domain.Core.SeedWork;
using FluentValidation.Results;
using Garnet.Standard.Pagination;

namespace Chat.Application.Services
{
    public interface IGroupService
    {
        Task<IPagedElements<GroupViewModel.GroupExpose>> GetAllPagination(IPagination pagination);
        Task<IPagedElements<GroupViewModel.MyGroup>> GetMyGroup(IPagination pagination, Guid userId);
        Task<GroupViewModel.GroupExpose> Get(int id);
        Task<GroupViewModel.GroupInfo> GetByName(string name);
        Task<CommandResponse<GroupViewModel.RegisterGroupOutput>> Register(GroupViewModel.RegisterGroup group);

        Task<ValidationResult> Update(GroupViewModel.UpdateGroup group);
        Task<ValidationResult> JoinGroup(GroupViewModel.JoinGroup joinGroup, Guid userId);
        Task<ValidationResult> LeaveGroup(GroupViewModel.LeaveGroup leaveGroup, Guid userId);

        Task<IPagedElements<MessageViewModel.MessageChatRoom>> GetMessagesGroup(IPagination pagination,
            string name, Guid currentUserId);
    }
}