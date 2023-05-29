using Garnet.Detail.Pagination.Asp.Extensions;
using Garnet.Standard.Pagination;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chat.Application.Services;
using Chat.Application.ViewModels;
using Chat.Extensions;


namespace Chat.Api.Controllers
{
    [Route("[controller]")]
    public class TicketsController : ApiController
    {
        private readonly ITicketService _ticketService;

        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet()]
        //[Authorize(Roles = (PrivilegeConstants.Chat_Ticket + "1," + PrivilegeConstants.AdminRolePrivilage))]
        public async Task<ActionResult<List<TicketViewModel.TicketExpose>>> GetAll(IPagination pagination)
        {
            var ticket = await _ticketService.GetAllPagination(pagination);
            return ticket.ToPaginationWithHeaderObjectResult();
        }

        [HttpPost]
        //[Authorize(Roles = (PrivilegeConstants.Chat_Ticket + "2," + PrivilegeConstants.AdminRolePrivilage))]
        public async Task<ActionResult<TicketViewModel.RegisterTicketOutput>> Post(
            [FromBody] TicketViewModel.RegisterTicket input)
        {
            return CustomResponse(await _ticketService.Register(input));
        }

        [HttpGet("{id}")]
        //[Authorize(Roles = (PrivilegeConstants.Chat_Ticket + "3," + PrivilegeConstants.AdminRolePrivilage))]
        public async Task<ActionResult<TicketViewModel.TicketAdditionalDataExpose>> GetById(int id)
        {
            return Ok(await _ticketService.Get(id));
        }

        [HttpPost("Join")]
        //[Authorize(Roles = (PrivilegeConstants.Chat_Channel + "2," + PrivilegeConstants.AdminRolePrivilage))]
        public async Task<IActionResult> JoinTicket([FromBody] TicketViewModel.JoinTicket join)
        {
            return CustomResponse(await _ticketService.JoinTicket(join, User.GetUserId()));
        }

        [HttpPut("Close")]
        //[Authorize(Roles = (PrivilegeConstants.Chat_Channel + "2," + PrivilegeConstants.AdminRolePrivilage))]
        public async Task<IActionResult> Close([FromBody] TicketViewModel.CloseTicket statusTicket)
        {
            return CustomResponse(await _ticketService.CloseTicket(statusTicket));
        }

        [HttpPut("Inprogress")]
        //[Authorize(Roles = (PrivilegeConstants.Chat_Channel + "2," + PrivilegeConstants.AdminRolePrivilage))]
        public async Task<IActionResult> Inprogress([FromBody] TicketViewModel.InprogressTicket statusTicket)
        {
            return CustomResponse(await _ticketService.InprogressTicket(statusTicket));
        }
        [HttpGet("{name}/messages")]
        //[Authorize(Roles = (PrivilegeConstants.Chat_Direct + "1," + PrivilegeConstants.AdminRolePrivilage))]
        public async Task<ActionResult<List<MessageViewModel.MessageChatRoom>>> GetMessagesChannel(
            [FromQuery] IPagination pagination, string name)
        {
            var direct = await _ticketService.GetMessagesGroup(pagination, name, User.GetUserId());
            return direct.ToPaginationWithHeaderObjectResult();
        }
    }
}