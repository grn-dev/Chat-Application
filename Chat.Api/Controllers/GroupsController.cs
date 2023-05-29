using Chat.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chat.Application.Services;
using Chat.Infra.Security.Extensions;
using Garnet.Standard.Pagination;
using Garnet.Detail.Pagination.Asp.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace Chat.Api.Controllers
{
    [Route("[controller]")]
    public class GroupsController : ApiController
    {
        private readonly IGroupService _groupService;

        public GroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet()]
        //[Authorize(Roles = (PrivilegeConstants.Chat_Group + "1," + PrivilegeConstants.AdminRolePrivilage))]
        public async Task<ActionResult<List<GroupViewModel.GroupExpose>>> GetAll([FromQuery] IPagination pagination)
        {
            var group = await _groupService.GetAllPagination(pagination);
            return group.ToPaginationWithHeaderObjectResult();
        }

        [HttpGet("{id}")]
        [Authorize] //(Roles = (PrivilegeConstants.Chat_Group + "3," + PrivilegeConstants.AdminRolePrivilage))]
        public async Task<ActionResult<GroupViewModel.GroupExpose>> GetById(int id)
        {
            return Ok(await _groupService.Get(id));
        }

        [HttpPost]
        //[Authorize(Roles = (PrivilegeConstants.Chat_Group + "2," + PrivilegeConstants.AdminRolePrivilage))]
        public async Task<ActionResult<GroupViewModel.RegisterGroupOutput>> Post(
            [FromBody] GroupViewModel.RegisterGroup input)
        {
            return CustomResponse(await _groupService.Register(input));
        }

        [HttpPut]
        //[Authorize(Roles = (PrivilegeConstants.Chat_Group + "5," + PrivilegeConstants.AdminRolePrivilage))]
        public async Task<IActionResult> Update([FromBody] GroupViewModel.UpdateGroup input)
        {
            return CustomResponse(await _groupService.Update(input));
        }

        [HttpPost("Join")]
        //[Authorize(Roles = (PrivilegeConstants.Chat_Channel + "2," + PrivilegeConstants.AdminRolePrivilage))]
        public async Task<IActionResult> JoinGroup(
            [FromBody] GroupViewModel.JoinGroup join)
        {
            return CustomResponse(await _groupService.JoinGroup(join, User.GetUserId()));
        }

        [HttpPost("Leave")]
        //[Authorize(Roles = (PrivilegeConstants.Chat_Channel + "2," + PrivilegeConstants.AdminRolePrivilage))]
        public async Task<IActionResult> LeaveGroup(
            [FromBody] GroupViewModel.LeaveGroup leave)
        {
            return CustomResponse(await _groupService.LeaveGroup(leave, User.GetUserId()));
        }

        [HttpGet("Mine")]
        //[Authorize(Roles = (PrivilegeConstants.Chat_Group + "1," + PrivilegeConstants.AdminRolePrivilage))]
        public async Task<ActionResult<List<GroupViewModel.MyGroup>>> GetAllMy([FromQuery] IPagination pagination)
        {
            var group = await _groupService.GetMyGroup(pagination, User.GetUserId());
            return group.ToPaginationWithHeaderObjectResult();
        }

        [HttpGet("Info/{name}")]
        //[Authorize(Roles = (PrivilegeConstants.Chat_Group + "3," + PrivilegeConstants.AdminRolePrivilage))]
        public async Task<ActionResult<GroupViewModel.GroupInfo>> GetById(string name)
        {
            return Ok(await _groupService.GetByName(name));
        }

        [HttpGet("{name}/messages")]
        //[Authorize(Roles = (PrivilegeConstants.Chat_Direct + "1," + PrivilegeConstants.AdminRolePrivilage))]
        public async Task<ActionResult<List<MessageViewModel.MessageChatRoom>>> GetMessagesChannel(
            [FromQuery] IPagination pagination, string name)
        {
            var direct = await _groupService.GetMessagesGroup(pagination, name, User.GetUserId());
            return direct.ToPaginationWithHeaderObjectResult();
        }
    }
}