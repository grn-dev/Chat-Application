using System;
using Chat.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chat.Application.Services;
using Chat.Extensions;
using Garnet.Standard.Pagination;
using Garnet.Detail.Pagination.Asp.Extensions;

namespace Chat.Api.Controllers
{
    [Route("[controller]")]
    public class DirectsController : ApiController
    {
        private readonly IDirectService _directService;

        public DirectsController(IDirectService directService)
        {
            _directService = directService;
        }

        [HttpGet()]
        //[Authorize(Roles = (PrivilegeConstants.Chat_Direct + "1," + PrivilegeConstants.AdminRolePrivilage))]
        public async Task<ActionResult<List<DirectViewModel.DirectExpose>>> GetAll([FromQuery] IPagination pagination)
        {
            var direct = await _directService.GetAllPagination(pagination);
            return direct.ToPaginationWithHeaderObjectResult();
        }

        [HttpGet("{id}")]
        //[Authorize(Roles = (PrivilegeConstants.Chat_Direct + "3," + PrivilegeConstants.AdminRolePrivilage))]
        public async Task<ActionResult<DirectViewModel.DirectExpose>> GetById(int id)
        {
            return Ok(await _directService.Get(id));
        }

        [HttpPost]
        //[Authorize(Roles = (PrivilegeConstants.Chat_Channel + "2," + PrivilegeConstants.AdminRolePrivilage))]
        public async Task<ActionResult<DirectViewModel.RegisterDirectOutput>> Post(
            [FromBody] DirectViewModel.RegisterDirect input)
        {
            return CustomResponse(await _directService.Register(input, User.GetUserId()));
        }

        [HttpGet("Mine")]
        //[Authorize(Roles = (PrivilegeConstants.Chat_Group + "1," + PrivilegeConstants.AdminRolePrivilage))]
        public async Task<ActionResult<List<DirectViewModel.MyDirect>>> GetAllMy([FromQuery] IPagination pagination)
        {
            var group = await _directService.GetMyDirect(pagination, User.GetUserId());
            return group.ToPaginationWithHeaderObjectResult();
        }

        [HttpGet("{name}/messages")]
        //[Authorize(Roles = (PrivilegeConstants.Chat_Direct + "1," + PrivilegeConstants.AdminRolePrivilage))]
        public async Task<ActionResult<List<MessageViewModel.MessageDirect>>> GetMessagesDirect(
            [FromQuery] IPagination pagination, string name)
        {
            var direct = await _directService.GetMessagesDirect(pagination, name);
            return direct.ToPaginationWithHeaderObjectResult();
        }
    }
}