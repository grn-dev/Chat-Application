using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Chat.Application.Configuration.Data.BasicQuery;
using Chat.Application.Configuration.Exceptions;
using Chat.Application.Services;
using Chat.Application.ViewModels;
using Chat.Domain.Attributes;
using Chat.Domain.Commands.ChatRoom;
using Chat.Domain.Commands.ChatRoomUser;
using Chat.Domain.Core.SeedWork;
using Chat.Domain.Models;
using Chat.Domain.Models.User;
using FluentValidation.Results;

namespace Chat.Application.Service.Services
{
    [Bean]
    public class ChatRoomService : ApplicationService, IChatRoomService
    {
        private readonly IMyMediatorHandler _mediator;
        private readonly IMapper _mapper;


        public ChatRoomService(
            IMyMediatorHandler mediatorHandler,
            IMapper mapper
        ) : base(mediatorHandler)
        {
            _mapper = mapper;
            _mediator = mediatorHandler;
        }


        public Task<ValidationResult> LeaveChatRoom(ChatRoomViewModel.LeaveChatRoom leave)
        {
            return SingleCommandExecutor(LeaveChatRoomCommand, leave);
        }

        public Task<CommandResponse<ChatRoomViewModel.RegisterChatRoomOutput>> Register(
            ChatRoomViewModel.RegisterChatRoom chatRoom)
        {
            return SingleCommandExecutorIncludeCommandReponse(LeaveChatRoomCommand1, chatRoom);
        }


        private async Task<CommandResponse<ChatRoomViewModel.RegisterChatRoomOutput>> LeaveChatRoomCommand1(
            ChatRoomViewModel.RegisterChatRoom chatRoom)
        {
            var result = await _mediator.SendCommand(new RegisterChatRoomCommand()
            {
                Topic = chatRoom.Topic,
                InviteCode = chatRoom.InviteCode,
                IsPrivate = chatRoom.IsPrivate,
            });

            return new CommandResponse<ChatRoomViewModel.RegisterChatRoomOutput>()
            {
                Response = new ChatRoomViewModel.RegisterChatRoomOutput()
                {
                    Id = result.Response.Id,
                    Name = result.Response.Name,
                    Topic = result.Response.Topic,
                },
                ValidationResult = result.ValidationResult
            };
        }


        private async Task<CommandResponse<int>> LeaveChatRoomCommand(ChatRoomViewModel.LeaveChatRoom leave)
        {
            var user = await _mediator.SendQuery(
                new GetPredicateToDestQuery<User, int, UserViewModel.UserBaseExpose>()
                {
                    Predicate = c => c.UserId == @leave.UserId
                });

            var room = await _mediator.SendQuery(
                new GetPredicateToDestQuery<ChatRoom, int, ChatRoomViewModel.ChatRoomExpose>()
                {
                    Predicate = c => c.Name == leave.Name
                });

            return await _mediator.SendCommand(new UpdateChatRoomUserStatusCommand()
            {
                UserId = user.Id,
                ChatRoomId = room.Id,
                Status = ChatRoomUserStatus.LEFT
            });
        }


        public Task<ValidationResult> JoinChatRoom(ChatRoomViewModel.JoinChatRoom chatRoom)
        {
            return SingleCommandExecutor(JoinRoomCommand, chatRoom);
        }

        private async Task<CommandResponse<int>> JoinRoomCommand(ChatRoomViewModel.JoinChatRoom @join)
        {
            var user = await _mediator.SendQuery(
                new GetPredicateToDestQuery<User, int, UserViewModel.UserBaseExpose>()
                {
                    Predicate = c => c.UserId == @join.UserId
                });

            var room = await _mediator.SendQuery(
                new GetPredicateToDestQuery<ChatRoom, int, ChatRoomViewModel.ChatRoomExpose>()
                {
                    Predicate = c => c.Name == @join.Name
                });

            if (room.IsPrivate)
            {
                // First, check if the invite code is correct
                if (!String.IsNullOrEmpty(@join.InviteCode) && String.Equals(@join.InviteCode,
                        room.InviteCode, StringComparison.OrdinalIgnoreCase))
                {
                    return await RegisterChatRoomUserCommand(user, room);
                }
                else
                {
                    throw new MyApplicationException(ApplicationErrorCode.CHAT_PRIVATE);
                }
            }

            return await RegisterChatRoomUserCommand(user, room);
        }

        private async Task<CommandResponse<int>> RegisterChatRoomUserCommand(UserViewModel.UserBaseExpose user,
            ChatRoomViewModel.ChatRoomExpose room)
        {
            return await _mediator.SendCommand(new RegisterChatRoomUserCommand()
            {
                UserId = user.Id,
                ChatRoomId = room.Id,
                Status = ChatRoomUserStatus.DEFAULT,
                IsAdmin = false
            });
        }


        public async Task<List<string>> GetRoomAndDirectNames(Guid userId)
        {
            List<string> list = new List<string>();

            list.AddRange(await _mediator.SendQuery(
                new GetAllByPredicateToDestQuery<ChatRoom, int, string>()
                {
                    Predicate = c =>
                        c.ChatRoomUsers.Any(x => x.User.UserId == userId && x.Status != ChatRoomUserStatus.LEFT)
                }));

            list.AddRange(await _mediator.SendQuery(
                new GetAllByPredicateToDestQuery<Direct, int, string>()
                {
                    Predicate = c =>
                        c.DirectUsers.Any(x => x.User.UserId == userId)
                }));

            return list;
        }
    }
}