using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chat.Application.ViewModels;
using Chat.Domain.Core.SeedWork;
using FluentValidation.Results;

namespace Chat.Application.Services
{
    public interface IChatRoomService
    {
        Task<ValidationResult> JoinChatRoom(ChatRoomViewModel.JoinChatRoom join);
        Task<ValidationResult> LeaveChatRoom(ChatRoomViewModel.LeaveChatRoom leave);
        Task<List<string>> GetRoomAndDirectNames(Guid userId); 

        Task<CommandResponse<ChatRoomViewModel.RegisterChatRoomOutput>> Register(
            ChatRoomViewModel.RegisterChatRoom chatRoom);
    }
}