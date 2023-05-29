using System;
using System.Threading.Tasks;
using Chat.Application.ViewModels;
using Chat.Domain.Core.SeedWork;
using FluentValidation.Results;
using Garnet.Standard.Pagination;

namespace Chat.Application.Services
{
    public interface IChannelService
    {
        Task<IPagedElements<ChannelViewModel.ChannelExpose>> GetAllPagination(IPagination pagination);
        Task<ChannelViewModel.ChannelExpose> Get(int id);

        Task<CommandResponse<ChannelViewModel.RegisterChannelOutput>>
            Register(ChannelViewModel.RegisterChannel channel);

        Task<ValidationResult> JoinChannel(ChannelViewModel.JoinChannel joinChannel, Guid userId);
        Task<ValidationResult> LeaveChannel(ChannelViewModel.LeaveChannel leaveChannel, Guid userId);

        Task<ValidationResult> Update(ChannelViewModel.UpdateChannel channel);

        Task<IPagedElements<ChannelViewModel.MyChannel>> GetMyChannel(IPagination pagination, Guid userId);
        Task<ChannelViewModel.ChannelInfo> GetByName(string name);

        Task<IPagedElements<MessageViewModel.MessageChatRoom>> GetMessagesChannel(IPagination pagination,
            string name, Guid currentUserId);
    }
}