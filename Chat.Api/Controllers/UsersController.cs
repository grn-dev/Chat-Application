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
    public class UsersController : ApiController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet()]
        //[Authorize(Roles = (PrivilegeConstants.Chat_User + "1," + PrivilegeConstants.AdminRolePrivilage))]
        public async Task<ActionResult<List<UserViewModel.UserExpose>>>GetAll([FromQuery] IPagination pagination)
        {  
            var user = await _userService.GetAllPagination(pagination); 
            return user.ToPaginationWithHeaderObjectResult(); 
        }

        [HttpGet("{id}")]
        //[Authorize(Roles = (PrivilegeConstants.Chat_User + "3," + PrivilegeConstants.AdminRolePrivilage))]
        public async Task<ActionResult<UserViewModel.UserExpose>> GetById(int id)
        {
            return Ok(await _userService.Get(id));
        }
        
        [HttpPost]
        //[Authorize(Roles = (PrivilegeConstants.Chat_User + "2," + PrivilegeConstants.AdminRolePrivilage))]
        public async Task<IActionResult> Post([FromBody] UserViewModel.RegisterUser input)
        {
            return CustomResponse(await _userService.Register(input));
        } 

        // [HttpPut]
        // //[Authorize(Roles = (PrivilegeConstants.Chat_User + "5," + PrivilegeConstants.AdminRolePrivilage))]
        // public async Task<IActionResult> Update([FromBody] UserViewModel.UpdateUser input)
        // {
        //     return CustomResponse(await _userService.Update(input));
        // } 

    }
}

