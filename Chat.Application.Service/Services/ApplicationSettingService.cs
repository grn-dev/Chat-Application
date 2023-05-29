using AutoMapper;
using FluentValidation.Results;
using Chat.Application.Services.ApplicationSetting;
using Chat.Application.ViewModels;
using Chat.Domain.Attributes;
using Chat.Domain.Commands.ApplicationSetting;
using Chat.Domain.Core.SeedWork;
using System.Threading.Tasks;
using Chat.Application.Configuration.Data.BasicQuery;
using Chat.Domain.Models;
using Garnet.Standard.Pagination; 

namespace Chat.Application.Service.Services
{
    [Bean]
    public class ApplicationSettingService : ApplicationService, IApplicationSettingService
    {
        private readonly IMyMediatorHandler _mediator;
        private readonly IMapper _mapper;


        public ApplicationSettingService(
            IMyMediatorHandler mediatorHandler,
            IMapper mapper
            ) : base(mediatorHandler)
        {
            _mapper = mapper;
            _mediator = mediatorHandler;
        }

        public Task<ApplicationSettingViewModel.ApplicationSettingExpose> Get()
        {
            return _mediator.SendQuery(
                    new GetSinglePredicateToDestQuery<ApplicationSetting, int, ApplicationSettingViewModel.ApplicationSettingExpose>()
                    {
                        Predicate = c => c.Status == ApplicationSettingsStatus.ACTIVE
                    });
        }
        public async Task<IPagedElements<ApplicationSettingViewModel.ApplicationSettingExpose>> GetAllPagination(IPagination pagination)
        {
            return await _mediator.SendQuery(
                    new PagableQuery<ApplicationSetting, int, ApplicationSettingViewModel.ApplicationSettingExpose>()
                    {
                        Pagination = pagination
                    });
        }
        public async Task<ValidationResult> Update(ApplicationSettingViewModel.UpdateApplicationSetting applicationSetting)
        {
            return await SingleCommandExecutor(UpdateCommand, applicationSetting);
        }
        private async Task<CommandResponse<int>> UpdateCommand(ApplicationSettingViewModel.UpdateApplicationSetting updateApplicationSetting)
        {
            var registerCommand = _mapper.Map<UpdateApplicationSettingCommand>(updateApplicationSetting);
            var result = await _mediator.SendCommand(registerCommand);
            return result;
        }
    }
}

