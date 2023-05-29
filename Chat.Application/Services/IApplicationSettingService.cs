using FluentValidation.Results;
using System.Threading.Tasks;
using Chat.Application.ViewModels;

namespace Chat.Application.Services.ApplicationSetting
{
    public interface IApplicationSettingService
    {
        Task<ApplicationSettingViewModel.ApplicationSettingExpose> Get();
        Task<ValidationResult> Update(ApplicationSettingViewModel.UpdateApplicationSetting applicationSetting);
    }
}



