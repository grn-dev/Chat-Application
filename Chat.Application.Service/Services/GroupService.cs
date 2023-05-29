using System;
using System.Linq;
using AutoMapper;
using FluentValidation.Results;
using Chat.Application.ViewModels;
using Chat.Domain.Attributes;
using Chat.Domain.Commands.Group;
using Chat.Domain.Core.SeedWork;
using System.Threading.Tasks;
using Chat.Application.Configuration.Data.BasicQuery;
using Chat.Application.Services;
using Chat.Domain.Models;
using Garnet.Standard.Pagination;

namespace Chat.Application.Service.Services
{ 
    [Bean]
    public class GroupService : ApplicationService, IGroupService
    {
        private readonly IMyMediatorHandler _mediator;
        private readonly IMapper _mapper;
        private readonly Lazy<IChatRoomService> _chatRoomService;


        public GroupService(
            IMyMediatorHandler mediatorHandler,
            IMapper mapper, Lazy<IChatRoomService> chatRoomService) : base(mediatorHandler)
        {
            _mapper = mapper;
            _chatRoomService = chatRoomService;
            _mediator = mediatorHandler;
        }

        public Task<IPagedElements<GroupViewModel.MyGroup>> GetMyGroup(IPagination pagination, Guid userId)
        {
            return _mediator.SendQuery(
                new PagablePredicateQuery<Group, int, GroupViewModel.MyGroup>()
                {
                    Pagination = pagination,
                    Predicate = x => x.ChatRoom.ChatRoomUsers.Any(c => c.User.UserId == userId)
                });
        }

        public async Task<GroupViewModel.GroupExpose> Get(int id)
        {
            return await _mediator.SendQuery(
                new GetPredicateToDestQuery<Group, int, GroupViewModel.GroupExpose>()
                {
                    Predicate = c => c.Id == id
                });
        }

        public Task<GroupViewModel.GroupInfo> GetByName(string name)
        {
            return _mediator.SendQuery(
                new GetPredicateToDestQuery<Group, int, GroupViewModel.GroupInfo>()
                {
                    Predicate = c => c.ChatRoom.Name == name
                });
        }

        public async Task<IPagedElements<GroupViewModel.GroupExpose>> GetAllPagination(IPagination pagination)
        {
            return await _mediator.SendQuery(
                new PagableQuery<Group, int, GroupViewModel.GroupExpose>()
                {
                    Pagination = pagination
                });
        }


        public async Task<CommandResponse<GroupViewModel.RegisterGroupOutput>> Register(
            GroupViewModel.RegisterGroup group)
        {
            return await SingleCommandExecutorIncludeCommandReponse(RegisterCommand, group);
        }

        private async Task<CommandResponse<GroupViewModel.RegisterGroupOutput>> RegisterCommand(
            GroupViewModel.RegisterGroup group)
        {
            var registerCommand = _mapper.Map<RegisterGroupCommand>(group);
            var result = await _mediator.SendCommand(registerCommand);
            return new CommandResponse<GroupViewModel.RegisterGroupOutput>()
            {
                Response = new GroupViewModel.RegisterGroupOutput()
                {
                    Id = result.Response.Id,
                    Name = result.Response.ChatRoom.Name,
                    Topic = result.Response.ChatRoom.Topic,
                },
                ValidationResult = result.ValidationResult
            };
        }

        public async Task<ValidationResult> Update(GroupViewModel.UpdateGroup group)
        {
            return await SingleCommandExecutor(UpdateCommand, group);
        }

        public Task<ValidationResult> JoinGroup(GroupViewModel.JoinGroup joinGroup, Guid userId)
        {
            return _chatRoomService.Value.JoinChatRoom(new ChatRoomViewModel.JoinChatRoom()
            {
                Name = joinGroup.Name,
                InviteCode = joinGroup.InviteCode,
                UserId = userId
            });
        }

        public Task<ValidationResult> LeaveGroup(GroupViewModel.LeaveGroup leaveGroup, Guid userId)
        {
            return _chatRoomService.Value.LeaveChatRoom(new ChatRoomViewModel.LeaveChatRoom()
            {
                Name = leaveGroup.Name,
                UserId = userId
            });
        }

        private async Task<CommandResponse<int>> UpdateCommand(GroupViewModel.UpdateGroup updateGroup)
        {
            var registerCommand = _mapper.Map<UpdateGroupCommand>(updateGroup);
            var result = await _mediator.SendCommand(registerCommand);
            return result;
        }

        public Task<IPagedElements<MessageViewModel.MessageChatRoom>> GetMessagesGroup(IPagination pagination,
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