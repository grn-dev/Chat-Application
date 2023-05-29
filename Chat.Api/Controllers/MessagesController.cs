using Chat.Application.ViewModels; 
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chat.Application.Services;
using Garnet.Standard.Pagination;
using Garnet.Detail.Pagination.Asp.Extensions;

namespace Chat.Api.Controllers
{
    [Route("[controller]")]
    public class MessagesController : ApiController
    {
        private readonly IMessageService _messageService;

        public MessagesController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet()]
        //[Authorize(Roles = (PrivilegeConstants.Chat_Message + "1," + PrivilegeConstants.AdminRolePrivilage))]
        public async Task<ActionResult<List<MessageViewModel.MessageExpose>>>GetAll([FromQuery] IPagination pagination)
        {  
            var message = await _messageService.GetAllPagination(pagination); 
            return message.ToPaginationWithHeaderObjectResult(); 
        }

        [HttpGet("{id}")]
        //[Authorize(Roles = (PrivilegeConstants.Chat_Message + "3," + PrivilegeConstants.AdminRolePrivilage))]
        public async Task<ActionResult<MessageViewModel.MessageExpose>> GetById(int id)
        {
            return Ok(await _messageService.Get(id));
        }
        
        // [HttpPost]
        // //[Authorize(Roles = (PrivilegeConstants.Chat_Message + "2," + PrivilegeConstants.AdminRolePrivilage))]
        // public async Task<IActionResult> Post([FromBody] MessageViewModel.AddMessageRegisterToRoom input)
        // {
        //     return CustomResponse(await _messageService.Register(input));
        // } 

        [HttpPut]
        //[Authorize(Roles = (PrivilegeConstants.Chat_Message + "5," + PrivilegeConstants.AdminRolePrivilage))]
        public async Task<IActionResult> Update([FromBody] MessageViewModel.UpdateMessage input)
        {
            return CustomResponse(await _messageService.Update(input));
        }

        // [HttpDelete("{id}")]
        // //[Authorize(Roles = (PrivilegeConstants.Chat_Message + "4," + PrivilegeConstants.AdminRolePrivilage))]
        // public async Task<IActionResult> Delete(int id)
        // {
        //     return CustomResponse(await _messageService.Delete(id));
        // }


    }
}

