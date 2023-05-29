using System;
using AutoMapper;
using FluentValidation.Results;
using Chat.Application.ViewModels;
using Chat.Domain.Attributes;
using Chat.Domain.Commands.Message;
using Chat.Domain.Core.SeedWork;
using System.Threading.Tasks;
using Chat.Application.Configuration.Data.BasicQuery;
using Chat.Application.Services;
using Chat.Domain.Models;
using Chat.Domain.Models.User;
using Garnet.Standard.Pagination;

namespace Chat.Application.Service.Services
{
    [Bean]
    public class MessageService : ApplicationService, IMessageService
    {
        private readonly IMyMediatorHandler _mediator;
        private readonly IMapper _mapper;


        public MessageService(
            IMyMediatorHandler mediatorHandler,
            IMapper mapper
        ) : base(mediatorHandler)
        {
            _mapper = mapper;
            _mediator = mediatorHandler;
        }

        public async Task<MessageViewModel.MessageExpose> Get(int id)
        {
            return await _mediator.SendQuery(
                new GetPredicateToDestQuery<Message, int, MessageViewModel.MessageExpose>()
                {
                    Predicate = c => c.Id == id
                });
        }

        public async Task<IPagedElements<MessageViewModel.MessageExpose>> GetAllPagination(IPagination pagination)
        {
            return await _mediator.SendQuery(
                new PagableQuery<Message, int, MessageViewModel.MessageExpose>()
                {
                    Pagination = pagination
                });
        }


        // public async Task<ValidationResult> Register(MessageViewModel.AddMessageRegisterToRoom message)
        // {
        //     return await SingleCommandExecutor(RegisterCommand, message);
        // }
        // private async Task<CommandResponse<int>> RegisterCommand(MessageViewModel.AddMessageRegisterToRoom message)
        // {
        //     var registerCommand = _mapper.Map<RegisterMessageChatRoomCommand>(message);
        //     var result = await _mediator.SendCommand(registerCommand);
        //     return result;
        // }

        public async Task<ValidationResult> Update(MessageViewModel.UpdateMessage message)
        {
            return await SingleCommandExecutor(UpdateCommand, message);
        }

        public async Task<ValidationResult> RegisterMessageToRoom(MessageViewModel.RegisterMessageToRoom room,
            Guid userId)
        {
            return await SingleCommandExecutor(SendMessageToRoomCommand, room, userId);
        }

        private async Task<CommandResponse<int>> SendMessageToRoomCommand(
            MessageViewModel.RegisterMessageToRoom chatMessageAdd, Guid userId)
        {
            var user = await _mediator.SendQuery(
                new GetPredicateToDestQuery<User, int, UserViewModel.UserBaseExpose>()
                {
                    Predicate = c => c.UserId == userId
                });

            var room = await _mediator.SendQuery(
                new GetPredicateToDestQuery<ChatRoom, int, ChatRoomViewModel.ChatRoomBaseExpose>()
                {
                    Predicate = c => c.Name == chatMessageAdd.RoomName
                });

            var message = await RegisterMessage(new MessageViewModel.RegisterMessage()
            {
                Content = chatMessageAdd.Content,
                Type = MessageType.DEFAULT,
                ParentId = chatMessageAdd.ParentId,
                UserId = user.Id,
                AttachmentId = chatMessageAdd.AttachmentId
            });

            var chatMessageCommand = new RegisterChatRoomMessageCommand()
            {
                SenderId = user.Id,
                ChatRoomId = room.Id,
                MessageId = message.Response,
                IsForwarded = false
            };

            return await _mediator.SendCommand(chatMessageCommand);
        }

        private async Task<CommandResponse<int>> RegisterMessage(MessageViewModel.RegisterMessage message)
        {
            var responseMessageCommand = await _mediator.SendCommand(_mapper.Map<RegisterMessageCommand>(message));
            return responseMessageCommand;
        }

        public async Task<ValidationResult> RegisterMessageToDirect(MessageViewModel.RegisterMessageToDirect direct,
            Guid userId)
        {
            return await SingleCommandExecutor(SendMessageDirectCommand, direct, userId);
        }

        private async Task<CommandResponse<int>> SendMessageDirectCommand(
            MessageViewModel.RegisterMessageToDirect direct, Guid userId)
        {
            var user = await _mediator.SendQuery(
                new GetPredicateToDestQuery<User, int, UserViewModel.UserBaseExpose>()
                {
                    Predicate = c => c.UserId == userId
                });

            var directExpose = await _mediator.SendQuery(
                new GetPredicateToDestQuery<Direct, int, DirectViewModel.DirectExpose>()
                {
                    Predicate = c => c.Name == direct.DirectName
                });

            var message = await RegisterMessage(new MessageViewModel.RegisterMessage()
            {
                Content = direct.Content,
                Type = MessageType.DEFAULT,
                ParentId = direct.ParentId,
                UserId = user.Id,
                AttachmentId = direct.AttachmentId
            });


            var chatMessageCommand = new RegisterDirectMessageCommand()
            {
                SenderId = user.Id,
                DirectId = directExpose.Id,
                MessageId = message.Response,
                IsForwarded = false
            };

            return await _mediator.SendCommand(chatMessageCommand);
        }


        private async Task<CommandResponse<int>> UpdateCommand(MessageViewModel.UpdateMessage updateMessage)
        {
            var registerCommand = _mapper.Map<UpdateMessageCommand>(updateMessage);
            var result = await _mediator.SendCommand(registerCommand);
            return result;
        }
    }
}