using AutoMapper;
using Chat.Domain.Commands.Ticket;
using Chat.Domain.Models.Ticket;
using static Chat.Application.ViewModels.TicketViewModel;

namespace Chat.Application.AutoMapper
{
    public class TicketProfile : Profile
    {
        public TicketProfile()
        {
            CreateMap<Ticket, TicketExpose>();
            CreateMap<Ticket, TicketAdditionalDataExpose>();
            CreateMap<RegisterTicket, RegisterTicketCommand>();
        }
    }
}