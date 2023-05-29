using System;
using System.Linq;
using AutoMapper;
using FluentValidation.Results;
using Chat.Domain.Attributes;
using Chat.Domain.Commands.Ticket;
using Chat.Domain.Core.SeedWork;
using System.Threading.Tasks;
using Chat.Application.Configuration.Data.BasicQuery;
using Chat.Domain.Models.Ticket;
using Chat.Domain.Models;
using static Chat.Application.ViewModels.UserViewModel;
using Chat.Application.Interfaces;
using Chat.Application.Configuration.Exceptions;
using Chat.Application.Services;
using Chat.Application.ViewModels;
using Chat.Domain.Commands.ChatRoom;
using Chat.Domain.Commands.ChatRoomUser;
using Chat.Domain.Models.User;
using Garnet.Standard.Pagination;

namespace Chat.Application.Service.Services
{
    [Bean]
    public class TicketService : ApplicationService, ITicketService
    {
        private readonly IMyMediatorHandler _mediator;
        private readonly IMapper _mapper;
        private readonly Lazy<IChatRoomService> _chatRoomService;


        public TicketService(
            IMyMediatorHandler mediatorHandler,
            IMapper mapper, Lazy<IChatRoomService> chatRoomService) : base(mediatorHandler)
        {
            _mapper = mapper;
            _chatRoomService = chatRoomService;
            _mediator = mediatorHandler;
        }

        public async Task<TicketViewModel.TicketAdditionalDataExpose> Get(int id)
        {
            return await _mediator.SendQuery(
                new GetPredicateToDestQuery<Ticket, int, TicketViewModel.TicketAdditionalDataExpose>()
                {
                    Predicate = c => c.Id == id
                });
        }

        public async Task<ValidationResult> JoinTicket(TicketViewModel.JoinTicket chatRoomJoinRoom, Guid userId)
        {
            var chatroomId = await CheckStatusTicket(chatRoomJoinRoom);


            return await SingleCommandExecutor(JoinRoomCommandTicket, new TicketViewModel.JoinRoomCommandTicket()
            {
                RoomName = chatRoomJoinRoom.RoomName,
                UserId = userId,
                ChatRoomId = chatroomId
            });
        }

        public async Task<IPagedElements<TicketViewModel.TicketExpose>> GetAllPagination(IPagination pagination)
        {
            return await _mediator.SendQuery(
                new PagableQuery<Ticket, int, TicketViewModel.TicketExpose>()
                {
                    Pagination = pagination
                });
        }

        public async Task<CommandResponse<TicketViewModel.RegisterTicketOutput>> Register(
            TicketViewModel.RegisterTicket ticket)
        {
            return await SingleCommandExecutorIncludeCommandReponse(RegisterCommand, ticket);
        }

        private async Task<CommandResponse<TicketViewModel.RegisterTicketOutput>> RegisterCommand(
            TicketViewModel.RegisterTicket ticket)
        {
            var registerCommand = _mapper.Map<RegisterTicketCommand>(ticket);

            var resultRegisterGroup = await _chatRoomService.Value.Register(new ChatRoomViewModel.RegisterChatRoom()
            {
                Topic = ticket.Subject,
                IsPrivate = false
            });
            registerCommand.ChatRoomId = resultRegisterGroup.Response.Id;

            var result = await _mediator.SendCommand(registerCommand);


            return new CommandResponse<TicketViewModel.RegisterTicketOutput>()
            {
                Response = new TicketViewModel.RegisterTicketOutput()
                {
                    Id = result.Response.Id,
                    Name = resultRegisterGroup.Response.Name,
                },
                ValidationResult = result.ValidationResult
            };
        }

        public async Task<ValidationResult> Delete(int id)
        {
            return await SingleCommandExecutor(DeleteCommand, id);
        }

        private async Task<CommandResponse<int>> DeleteCommand(int id)
        {
            var result = await _mediator.SendCommand(new DeleteTicketCommand()
            {
                Id = id
            });
            return result;
        }

        private async Task<CommandResponse<int>> JoinRoomCommandTicket(
            TicketViewModel.JoinRoomCommandTicket ticket)
        {
            var user = await _mediator.SendQuery(
                new GetPredicateToDestQuery<User, int, UserExpose>()
                {
                    Predicate = c => c.UserId == ticket.UserId
                });

            var resCommand = await _mediator.SendCommand(new RegisterChatRoomUserCommand()
            {
                UserId = user.Id,
                ChatRoomId = ticket.ChatRoomId,
                Status = ChatRoomUserStatus.DEFAULT
            });

            return resCommand;
        }

        private async Task<int> CheckStatusTicket(TicketViewModel.JoinTicket chatRoomJoinRoom)
        {
            var ticket = await _mediator.SendQuery(
                new GetPredicateToDestQuery<Ticket, int, TicketViewModel.TicketAdditionalDataExpose>()
                {
                    Predicate = c => c.ChatRoom.Name == chatRoomJoinRoom.RoomName
                });

            if (ticket.TicketStatus == TicketStatus.INPROGRESS)
                throw new MyApplicationException(ApplicationErrorCode.TICKET_INPROGRESS);

            return ticket.ChatRoom.Id;
        }

        public async Task<ValidationResult> CloseTicket(TicketViewModel.CloseTicket closeTicket)
        {
            return await SingleCommandExecutor(ChangeStatusTicketCommand, closeTicket);
        }

        public Task<ValidationResult> InprogressTicket(TicketViewModel.InprogressTicket closeTicket)
        {
            return SingleCommandExecutor(InprogressTicketCommand, closeTicket);
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

        private async Task<CommandResponse<int>> InprogressTicketCommand(
            TicketViewModel.InprogressTicket inprogressTicket)
        {
            var result = await _mediator.SendCommand(new ChangeTicketStatusCommand()
            {
                TicketStatus = TicketStatus.INPROGRESS,
                Id = inprogressTicket.Id
            });

            return result;
        }

        private async Task<CommandResponse<int>> ChangeStatusTicketCommand(
            TicketViewModel.CloseTicket closeTicket)
        {
            var result = await _mediator.SendCommand(new ChangeTicketStatusCommand()
            {
                TicketStatus = TicketStatus.CLOSE,
                Id = closeTicket.Id
            });

            return result;
        }
    }
}