using System;
using AutoMapper;
using FluentValidation.Results;
using Chat.Application.ViewModels;
using Chat.Domain.Attributes;
using Chat.Domain.Commands.User;
using Chat.Domain.Core.SeedWork;
using System.Threading.Tasks;
using Chat.Application.Configuration.Data.BasicQuery;
using Chat.Application.Services;
using Chat.Domain.Models.User;
using Garnet.Standard.Pagination;

namespace Chat.Application.Service.Services
{
    [Bean]
    public class UserService : ApplicationService, IUserService
    {
        private readonly IMyMediatorHandler _mediator;
        private readonly IMapper _mapper;


        public UserService(
            IMyMediatorHandler mediatorHandler,
            IMapper mapper
        ) : base(mediatorHandler)
        {
            _mapper = mapper;
            _mediator = mediatorHandler;
        }

        public async Task<UserViewModel.UserExpose> Get(int id)
        {
            return await _mediator.SendQuery(
                new GetPredicateToDestQuery<User, int, UserViewModel.UserExpose>()
                {
                    Predicate = c => c.Id == id
                });
        }

        public Task<UserViewModel.UserBaseExpose> Get(Guid userId)
        {
            return _mediator.SendQuery(
                new GetPredicateToDestQuery<User, int, UserViewModel.UserBaseExpose>()
                {
                    Predicate = c => c.UserId == userId
                });
        }

        public async Task<IPagedElements<UserViewModel.UserExpose>> GetAllPagination(IPagination pagination)
        {
            return await _mediator.SendQuery(
                new PagableQuery<User, int, UserViewModel.UserExpose>()
                {
                    Pagination = pagination
                });
        }


        public async Task<ValidationResult> Register(UserViewModel.RegisterUser user)
        {
            return await SingleCommandExecutor(RegisterCommand, user);
        }

        private async Task<CommandResponse<int>> RegisterCommand(UserViewModel.RegisterUser user)
        {
            var registerCommand = _mapper.Map<RegisterUserCommand>(user);
            var result = await _mediator.SendCommand(registerCommand);
            return result;
        }

        public async Task<ValidationResult> Update(UserViewModel.UpdateUser user)
        {
            return await SingleCommandExecutor(UpdateCommand, user);
        }

        private async Task<CommandResponse<int>> UpdateCommand(UserViewModel.UpdateUser updateUser)
        {
            var registerCommand = _mapper.Map<UpdateUserCommand>(updateUser);
            var result = await _mediator.SendCommand(registerCommand);
            return result;
        }

        public Task UpdateActivity(string userId)
        {
            _mediator.SendCommand(new UpdateActivityCommand()
            {
                UserId = new Guid(userId)
            });
            return Task.CompletedTask;
        }
    }
}