using AutoMapper; 
using Chat.Application.ViewModels;
using Chat.Domain.Commands.ApplicationSetting;
using Chat.Domain.Models;

namespace Chat.Application.AutoMapper
{
    public class ApplicationSettingProfile : Profile
    {
        public ApplicationSettingProfile()    
        {
            CreateMap<ApplicationSetting, ApplicationSettingViewModel.ApplicationSettingExpose>();   
            CreateMap<ApplicationSettingViewModel.UpdateApplicationSetting, UpdateApplicationSettingCommand>(); 
        }
    } 
}

