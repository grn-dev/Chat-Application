using AutoMapper;
using Chat.Application.ViewModels;
using Chat.Domain.Models.Ticket;

namespace Chat.Application.AutoMapper
{
    public class TicketCategoryProfile : Profile
    {
        public TicketCategoryProfile()    
        {
            CreateMap<TicketCategory, TicketCategoryViewModel.TicketCategoryExpose>();
        }
    } 
}

