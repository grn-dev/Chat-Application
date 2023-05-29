using Chat.Application.Services.ApplicationSetting;
using Chat.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Chat.Api.Controllers
{
    [Route("[controller]")]
    public class ApplicationSettingsController : ApiController
    {
        private readonly IApplicationSettingService _applicationSettingService;

        public ApplicationSettingsController(IApplicationSettingService applicationSettingService)
        {
            _applicationSettingService = applicationSettingService;
        }

        [HttpGet()]
        //[Authorize(Roles = (PrivilegeConstants.Chat_ApplicationSetting + "3," + PrivilegeConstants.AdminRolePrivilage))]
        public async Task<ActionResult<ApplicationSettingViewModel.ApplicationSettingExpose>> GetById()
        {
            return Ok(await _applicationSettingService.Get());
        }
        [HttpPut]
        //[Authorize(Roles = (PrivilegeConstants.Chat_ApplicationSetting + "5," + PrivilegeConstants.AdminRolePrivilage))]
        public async Task<IActionResult> Update([FromBody] ApplicationSettingViewModel.UpdateApplicationSetting input)
        {
            return CustomResponse(await _applicationSettingService.Update(input));
        }
    }
}

