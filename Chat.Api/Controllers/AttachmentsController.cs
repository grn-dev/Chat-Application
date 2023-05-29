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
    public class AttachmentsController : ApiController
    {
        private readonly IAttachmentService _attachmentService;

        public AttachmentsController(IAttachmentService attachmentService)
        {
            _attachmentService = attachmentService;
        }

        [HttpGet()]
        //[Authorize(Roles = (PrivilegeConstants.Chat_Attachment + "1," + PrivilegeConstants.AdminRolePrivilage))]
        public async Task<ActionResult<List<AttachmentViewModel.AttachmentExpose>>> GetAll(
            [FromQuery] IPagination pagination)
        {
            var attachment = await _attachmentService.GetAllPagination(pagination);
            return attachment.ToPaginationWithHeaderObjectResult();
        }

        [HttpGet("{id}")]
        //[Authorize(Roles = (PrivilegeConstants.Chat_Attachment + "3," + PrivilegeConstants.AdminRolePrivilage))]
        public async Task<ActionResult<AttachmentViewModel.AttachmentExpose>> GetById(int id)
        {
            return Ok(await _attachmentService.Get(id));
        }

        [HttpPost("Direct")]
        //[Authorize(Roles = (PrivilegeConstants.Chat_Attachment + "2," + PrivilegeConstants.AdminRolePrivilage))]
        public async Task<IActionResult> Post([FromBody] AttachmentViewModel.RegisterAttachmentToDirect input)
        {
            return CustomResponse(await _attachmentService.RegisterToDirect(input));
        }

        [HttpPost("Room")]
        //[Authorize(Roles = (PrivilegeConstants.Chat_Attachment + "2," + PrivilegeConstants.AdminRolePrivilage))]
        public async Task<IActionResult> Post([FromBody] AttachmentViewModel.RegisterAttachmentToRoom input)
        {
            return CustomResponse(await _attachmentService.RegisterToRoom(input));
        }
    }
}