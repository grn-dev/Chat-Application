using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Chat.Application.ViewModels;
using Chat.Domain.Attributes;
using Chat.Domain.Core.SeedWork;
using System.Threading.Tasks;
using Chat.Application.Configuration.Data.BasicQuery;
using Chat.Application.Configuration.Exceptions;
using Chat.Application.Services;
using Chat.Domain.Commands.Direct;
using Chat.Domain.Models;
using Chat.Domain.Models.User;
using FluentValidation.Results;
using Garnet.Standard.Pagination;

namespace Chat.Application.Service.Services
{
    [Bean]
    public class DirectService : ApplicationService, IDirectService
    {
        private readonly IMyMediatorHandler _mediator;
        private readonly IMapper _mapper;


        public DirectService(
            IMyMediatorHandler mediatorHandler,
            IMapper mapper
        ) : base(mediatorHandler)
        {
            _mapper = mapper;
            _mediator = mediatorHandler;
        }

        public Task<IPagedElements<MessageViewModel.MessageDirect>> GetMessagesDirect(IPagination pagination,
            string name)
        {
            return _mediator.SendQuery(
                new PagablePredicateQuery<DirectMessage, int, MessageViewModel.MessageDirect>()
                {
                    Pagination = pagination,
                    Predicate = x => x.Direct.Name == name
                });
        }

        public async Task<DirectViewModel.DirectExpose> Get(int id)
        {
            return await _mediator.SendQuery(
                new GetPredicateToDestQuery<Direct, int, DirectViewModel.DirectExpose>()
                {
                    Predicate = c => c.Id == id
                });
        }

        public async Task<CommandResponseBatchValidationResult<DirectViewModel.RegisterDirectOutput>> Register(
            DirectViewModel.RegisterDirect direct, Guid currentUserId)
        {
            return await CommandExecutorIncludeCommandReponse(RegisterCommand, direct, currentUserId);
        }

        private async Task<CommandResponseBatchValidationResult<DirectViewModel.RegisterDirectOutput>> RegisterCommand(
            DirectViewModel.RegisterDirect direct, Guid currentUserId)
        {
            var sourceUser = await _mediator.SendQuery(
                new GetPredicateToDestQuery<User, int, UserViewModel.UserBaseExpose>()
                {
                    Predicate = c => c.UserId == currentUserId
                });

            var destinationUser = await _mediator.SendQuery(
                new GetPredicateToDestQuery<User, int, UserViewModel.UserBaseExpose>()
                {
                    Predicate = c => c.UserId == direct.UserId
                });

            await CheckExistDirectUser(destinationUser, sourceUser);

            IList<ValidationResult> validationResults = new List<ValidationResult>();

            var directResponse = await _mediator.SendCommand(new RegisterDirectCommand());


            var sourceDirectUser = await _mediator.SendCommand(new RegisterDirectUserCommand()
            {
                UserId = destinationUser.Id,
                DirectId = directResponse.Response.Id
            });
            validationResults.Add(sourceDirectUser.ValidationResult);


            var destinationDirectUser = await _mediator.SendCommand(new RegisterDirectUserCommand()
            {
                UserId = sourceUser.Id,
                DirectId = directResponse.Response.Id
            });
            validationResults.Add(destinationDirectUser.ValidationResult);


            return new CommandResponseBatchValidationResult<DirectViewModel.RegisterDirectOutput>()
            {
                Response = new DirectViewModel.RegisterDirectOutput()
                {
                    Id = directResponse.Response.Id,
                    Name = directResponse.Response.Name
                },
                ValidationResults = validationResults
            };
        }

        private async Task CheckExistDirectUser(UserViewModel.UserBaseExpose destinationUser,
            UserViewModel.UserBaseExpose sourceUser)
        {
            var exist = await _mediator.SendQuery(
                new AnyByPredicateQuery<Direct, int>()
                {
                    Predicate = c => c.DirectUsers.Any(x => x.UserId == sourceUser.Id) &&
                                     c.DirectUsers.Any(x => x.UserId == destinationUser.Id)
                });

            if (exist)
                throw new MyApplicationException(ApplicationErrorCode.EXIST_DIRECT_USER);
        }

        public async Task<IPagedElements<DirectViewModel.DirectExpose>> GetAllPagination(IPagination pagination)
        {
            return await _mediator.SendQuery(
                new PagableQuery<Direct, int, DirectViewModel.DirectExpose>()
                {
                    Pagination = pagination
                });
        }

        public Task<IPagedElements<DirectViewModel.MyDirect>> GetMyDirect(IPagination pagination, Guid currentUserId)
        {
            return _mediator.SendQuery(
                new PagablePredicateQuery<Direct, int, DirectViewModel.MyDirect>()
                {
                    Pagination = pagination,
                    Predicate = x => x.DirectUsers.Any(c => c.User.UserId == currentUserId)
                });
        }


        // public async Task<ValidationResult> Register(DirectViewModel.RegisterDirect direct)
        // {
        //     return await SingleCommandExecutor(RegisterCommand, direct);
        // }
        //
        // private async Task<CommandResponse<int>> RegisterCommand(DirectViewModel.RegisterDirect direct)
        // {
        //     var registerCommand = _mapper.Map<RegisterDirectCommand>(direct);
        //     var result = await _mediator.SendCommand(registerCommand);
        //     return result;
        // }

        // public async Task<ValidationResult> Update(DirectViewModel.UpdateDirect direct)
        // {
        //     return await SingleCommandExecutor(UpdateCommand, direct);
        // }
        //
        // private async Task<CommandResponse<int>> UpdateCommand(DirectViewModel.UpdateDirect updateDirect)
        // {
        //     var registerCommand = _mapper.Map<UpdateDirectCommand>(updateDirect);
        //     var result = await _mediator.SendCommand(registerCommand);
        //     return result;
        // }
    }
}