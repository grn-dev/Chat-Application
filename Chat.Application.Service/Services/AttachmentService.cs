using System;
using System.Collections.Generic;
using AutoMapper;
using FluentValidation.Results;
using Chat.Application.ViewModels;
using Chat.Domain.Attributes;
using Chat.Domain.Commands.Attachment;
using Chat.Domain.Core.SeedWork;
using System.Threading.Tasks;
using Chat.Application.Configuration.Data.BasicQuery;
using Chat.Application.Interfaces;
using Chat.Application.Services;
using Chat.Domain.Models;
using Garnet.Standard.Pagination;

namespace Chat.Application.Service.Services
{
    [Bean]
    public class AttachmentService : ApplicationService, IAttachmentService
    {
        private readonly IMyMediatorHandler _mediator;
        private readonly Lazy<IMessageService> _messageService;
        private readonly Lazy<ICurrentUserService> _currentUserService;
        private readonly IMapper _mapper;


        public AttachmentService(
            IMyMediatorHandler mediatorHandler,
            IMapper mapper, Lazy<IMessageService> messageService, Lazy<ICurrentUserService> currentUserService) : base(
            mediatorHandler)
        {
            _mapper = mapper;
            _messageService = messageService;
            _currentUserService = currentUserService;
            _mediator = mediatorHandler;
        }

        public async Task<AttachmentViewModel.AttachmentExpose> Get(int id)
        {
            return await _mediator.SendQuery(
                new GetPredicateToDestQuery<Attachment, int, AttachmentViewModel.AttachmentExpose>()
                {
                    Predicate = c => c.Id == id
                });
        }

        public Task<ValidationResult> RegisterToDirect(AttachmentViewModel.RegisterAttachmentToDirect attachment)
        {
            return CommandExecutor(RegisterCommandToDirect, attachment);
        }

        private async Task<IList<ValidationResult>> RegisterCommandToDirect(
            AttachmentViewModel.RegisterAttachmentToDirect attachment)
        {
            IList<ValidationResult> validationResults = new List<ValidationResult>();

            var registerCommand = _mapper.Map<RegisterAttachmentCommand>(attachment);
            var result = await _mediator.SendCommand(registerCommand);

            validationResults.Add(result.ValidationResult);

            var messageToDirect = await _messageService.Value.RegisterMessageToDirect(
                new MessageViewModel.RegisterMessageToDirect()
                {
                    Content = attachment.Message.Content,
                    ParentId = attachment.Message.ParentId,
                    DirectName = attachment.Message.DirectName,
                    AttachmentId = result.Response
                }, _currentUserService.Value.UserId.Value);

            validationResults.Add(messageToDirect);

            return validationResults;
        }

        public Task<ValidationResult> RegisterToRoom(AttachmentViewModel.RegisterAttachmentToRoom attachment)
        {
            return CommandExecutor(RegisterCommandToRoom, attachment);
        }

        private async Task<IList<ValidationResult>> RegisterCommandToRoom(
            AttachmentViewModel.RegisterAttachmentToRoom attachment)
        {
            IList<ValidationResult> validationResults = new List<ValidationResult>();

            var registerCommand = _mapper.Map<RegisterAttachmentCommand>(attachment);
            var result = await _mediator.SendCommand(registerCommand);
            validationResults.Add(result.ValidationResult);

            var registerMessageToRoom = await _messageService.Value.RegisterMessageToRoom(
                new MessageViewModel.RegisterMessageToRoom()
                {
                    Content = attachment.Message.Content,
                    ParentId = attachment.Message.ParentId,
                    RoomName = attachment.Message.RoomName,
                    AttachmentId = result.Response,
                }, _currentUserService.Value.UserId.Value);
            validationResults.Add(registerMessageToRoom);

            return validationResults;
        }

        public async Task<IPagedElements<AttachmentViewModel.AttachmentExpose>> GetAllPagination(IPagination pagination)
        {
            return await _mediator.SendQuery(
                new PagableQuery<Attachment, int, AttachmentViewModel.AttachmentExpose>()
                {
                    Pagination = pagination
                });
        }
    }
}