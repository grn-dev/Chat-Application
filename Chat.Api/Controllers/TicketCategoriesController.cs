using Chat.Application.Services.TicketCategory;
using Chat.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Garnet.Standard.Pagination;
using Garnet.Detail.Pagination.Asp.Extensions;

namespace Chat.Api.Controllers
{
    [Route("[controller]")]
    public class TicketCategoriesController : ApiController
    {
        private readonly ITicketCategoryService _ticketCategoryService;

        public TicketCategoriesController(ITicketCategoryService ticketCategoryService)
        {
            _ticketCategoryService = ticketCategoryService;
        }

        [HttpGet()]
        //[Authorize(Roles = (PrivilegeConstants.Chat_TicketCategory + "1," + PrivilegeConstants.AdminRolePrivilage))]
        public async Task<ActionResult<List<TicketCategoryViewModel.TicketCategoryExpose>>> GetAll([FromQuery] IPagination pagination)
        {
            var ticketCategory = await _ticketCategoryService.GetAllPagination(pagination);
            return ticketCategory.ToPaginationWithHeaderObjectResult();
        }

        [HttpGet("{id}")]
        //[Authorize(Roles = (PrivilegeConstants.Chat_TicketCategory + "3," + PrivilegeConstants.AdminRolePrivilage))]
        public async Task<ActionResult<TicketCategoryViewModel.TicketCategoryExpose>> GetById(int id)
        {
            return Ok(await _ticketCategoryService.Get(id));
        }

        [HttpPost]
        //[Authorize(Roles = (PrivilegeConstants.Chat_TicketCategory + "2," + PrivilegeConstants.AdminRolePrivilage))]
        public async Task<IActionResult> Post([FromBody] TicketCategoryViewModel.RegisterTicketCategory input)
        {
            return CustomResponse(await _ticketCategoryService.Register(input));
        }

        [HttpPut]
        //[Authorize(Roles = (PrivilegeConstants.Chat_TicketCategory + "5," + PrivilegeConstants.AdminRolePrivilage))]
        public async Task<IActionResult> Update([FromBody] TicketCategoryViewModel.UpdateTicketCategory input)
        {
            return CustomResponse(await _ticketCategoryService.Update(input));
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = (PrivilegeConstants.Chat_TicketCategory + "4," + PrivilegeConstants.AdminRolePrivilage))]
        public async Task<IActionResult> Delete(int id)
        {
            return CustomResponse(await _ticketCategoryService.Delete(id));
        }


    }
}

