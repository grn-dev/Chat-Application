using System;
using System.Linq;
using AutoMapper;
using FluentValidation.Results;
using Chat.Application.ViewModels;
using Chat.Domain.Attributes;
using Chat.Domain.Commands.Channel;
using Chat.Domain.Core.SeedWork;
using System.Threading.Tasks;
using Chat.Application.Configuration.Data.BasicQuery;
using Chat.Application.Services;
using Chat.Domain.Models;
using Garnet.Standard.Pagination;

namespace Chat.Application.Service.Services
{ 
    [Bean]
    public class ChannelService : ApplicationService, IChannelService
    {
        private readonly IMyMediatorHandler _mediator;
        private readonly IMapper _mapper;
        private readonly Lazy<IChatRoomService> _chatRoomService;


        public ChannelService(
            IMyMediatorHandler mediatorHandler,
            IMapper mapper, Lazy<IChatRoomService> chatRoomService) : base(mediatorHandler)
        {
            _mapper = mapper;
            _chatRoomService = chatRoomService;
            _mediator = mediatorHandler;
        }

        public async Task<ChannelViewModel.ChannelExpose> Get(int id)
        {
            return await _mediator.SendQuery(
                new GetPredicateToDestQuery<Domain.Models.Channel, int, ChannelViewModel.ChannelExpose>()
                {
                    Predicate = c => c.Id == id
                });
        }

        public async Task<IPagedElements<ChannelViewModel.ChannelExpose>> GetAllPagination(IPagination pagination)
        {
            return await _mediator.SendQuery(
                new PagableQuery<Domain.Models.Channel, int, ChannelViewModel.ChannelExpose>()
                {
                    Pagination = pagination
                });
        }


        public async Task<CommandResponse<ChannelViewModel.RegisterChannelOutput>> Register(
            ChannelViewModel.RegisterChannel channel)
        {
            return await SingleCommandExecutorIncludeCommandReponse(RegisterCommand, channel);
        }

        public Task<ValidationResult> JoinChannel(ChannelViewModel.JoinChannel joinChannel, Guid userId)
        {
            return _chatRoomService.Value.JoinChatRoom(new ChatRoomViewModel.JoinChatRoom()
            {
                Name = joinChannel.Name,
                InviteCode = joinChannel.InviteCode,
                UserId = userId
            });
        }

        public Task<ValidationResult> LeaveChannel(ChannelViewModel.LeaveChannel leaveChannel, Guid userId)
        {
            return _chatRoomService.Value.LeaveChatRoom(new ChatRoomViewModel.LeaveChatRoom()
            {
                Name = leaveChannel.Name,
                UserId = userId
            });
        }

        private async Task<CommandResponse<ChannelViewModel.RegisterChannelOutput>> RegisterCommand(
            ChannelViewModel.RegisterChannel channel)
        {
            var registerCommand = _mapper.Map<RegisterChannelCommand>(channel);
            var result = await _mediator.SendCommand(registerCommand);
            return new CommandResponse<ChannelViewModel.RegisterChannelOutput>()
            {
                Response = new ChannelViewModel.RegisterChannelOutput()
                {
                    Id = result.Response.Id,
                    Name = result.Response.ChatRoom.Name,
                    Topic = result.Response.ChatRoom.Topic,
                },
                ValidationResult = result.ValidationResult
            };
        }

        public async Task<ValidationResult> Update(ChannelViewModel.UpdateChannel channel)
        {
            return await SingleCommandExecutor(UpdateCommand, channel);
        }

        public Task<IPagedElements<ChannelViewModel.MyChannel>> GetMyChannel(IPagination pagination, Guid userId)
        {
            return _mediator.SendQuery(
                new PagablePredicateQuery<Channel, int, ChannelViewModel.MyChannel>()
                {
                    Pagination = pagination,
                    Predicate = x => x.ChatRoom.ChatRoomUsers.Any(c => c.User.UserId == userId)
                });
        }

        public Task<ChannelViewModel.ChannelInfo> GetByName(string name)
        {
            return _mediator.SendQuery(
                new GetPredicateToDestQuery<Channel, int, ChannelViewModel.ChannelInfo>()
                {
                    Predicate = c => c.ChatRoom.Name == name
                });
        }

        private async Task<CommandResponse<int>> UpdateCommand(ChannelViewModel.UpdateChannel updateChannel)
        {
            var registerCommand = _mapper.Map<UpdateChannelCommand>(updateChannel);
            var result = await _mediator.SendCommand(registerCommand);
            return result;
        }

        public Task<IPagedElements<MessageViewModel.MessageChatRoom>> GetMessagesChannel(IPagination pagination,
            string name, Guid currentUserId)
        {
            return _mediator.SendQuery(
                new PagablePredicateQuery<ChatRoomMessage, int, MessageViewModel.MessageChatRoom>()
                {
                    Pagination = pagination,
                    Predicate = x => x.ChatRoom.ChatRoomUsers.Any(x => x.User.UserId == currentUserId)
                                     && x.ChatRoom.Name == name
                });
        }
    }
}