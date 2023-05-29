using Chat.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chat.Application.Services;
using Chat.Extensions;
using Garnet.Standard.Pagination;
using Garnet.Detail.Pagination.Asp.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace Chat.Api.Controllers
{
    [Route("[controller]")]
    public class ChannelsController : ApiController
    {
        private readonly IChannelService _channelService;

        public ChannelsController(IChannelService channelService)
        {
            _channelService = channelService;
        }

        [HttpGet()]
        //[Authorize(Roles = (PrivilegeConstants.Chat_Channel + "1," + PrivilegeConstants.AdminRolePrivilage))]
        public async Task<ActionResult<List<ChannelViewModel.ChannelExpose>>> GetAll([FromQuery] IPagination pagination)
        {
            var channel = await _channelService.GetAllPagination(pagination);
            return channel.ToPaginationWithHeaderObjectResult();
        }

        [HttpGet("{id}")]
        //[Authorize(Roles = (PrivilegeConstants.Chat_Channel + "3," + PrivilegeConstants.AdminRolePrivilage))]
        public async Task<ActionResult<ChannelViewModel.ChannelExpose>> GetById(int id)
        {
            return Ok(await _channelService.Get(id));
        }

        [HttpPost]
        [Authorize]//(Roles = (PrivilegeConstants.Chat_Channel + "2," + PrivilegeConstants.AdminRolePrivilage))]
        public async Task<ActionResult<ChannelViewModel.RegisterChannelOutput>> Post(
            [FromBody] ChannelViewModel.RegisterChannel input)
        {
            return CustomResponse(await _channelService.Register(input));
        }

        [HttpPost("Join")]
        //[Authorize(Roles = (PrivilegeConstants.Chat_Channel + "2," + PrivilegeConstants.AdminRolePrivilage))]
        public async Task<IActionResult> JoinChannel(
            [FromBody] ChannelViewModel.JoinChannel join)
        {
            return CustomResponse(await _channelService.JoinChannel(join, User.GetUserId()));
        }

        [HttpPost("Leave")]
        //[Authorize(Roles = (PrivilegeConstants.Chat_Channel + "2," + PrivilegeConstants.AdminRolePrivilage))]
        public async Task<IActionResult> LeaveChannel(
            [FromBody] ChannelViewModel.LeaveChannel leave)
        {
            return CustomResponse(await _channelService.LeaveChannel(leave, User.GetUserId()));
        }

        [HttpPut]
        //[Authorize(Roles = (PrivilegeConstants.Chat_Channel + "5," + PrivilegeConstants.AdminRolePrivilage))]
        public async Task<IActionResult> Update([FromBody] ChannelViewModel.UpdateChannel input)
        {
            return CustomResponse(await _channelService.Update(input));
        }

        [HttpGet("Mine")]
        //[Authorize(Roles = (PrivilegeConstants.Chat_Group + "1," + PrivilegeConstants.AdminRolePrivilage))]
        public async Task<ActionResult<List<ChannelViewModel.MyChannel>>> GetAllMy([FromQuery] IPagination pagination)
        {
            var group = await _channelService.GetMyChannel(pagination, User.GetUserId());
            return group.ToPaginationWithHeaderObjectResult();
        }

        [HttpGet("Info/{name}")]
        //[Authorize(Roles = (PrivilegeConstants.Chat_Group + "3," + PrivilegeConstants.AdminRolePrivilage))]
        public async Task<ActionResult<ChannelViewModel.ChannelInfo>> GetById(string name)
        {
            return Ok(await _channelService.GetByName(name));
        }

        [HttpGet("{name}/messages")]
        //[Authorize(Roles = (PrivilegeConstants.Chat_Direct + "1," + PrivilegeConstants.AdminRolePrivilage))]
        public async Task<ActionResult<List<MessageViewModel.MessageChatRoom>>> GetMessagesChannel(
            [FromQuery] IPagination pagination, string name)
        {
            var direct = await _channelService.GetMessagesChannel(pagination, name, User.GetUserId());
            return direct.ToPaginationWithHeaderObjectResult();
        }
    }
}