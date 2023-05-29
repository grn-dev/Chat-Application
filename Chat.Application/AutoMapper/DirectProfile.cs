using System.Linq;
using AutoMapper;
using Chat.Application.ViewModels;
using Chat.Domain.Models;

namespace Chat.Application.AutoMapper
{
    public class DirectProfile : Profile
    {
        public DirectProfile()
        {
            CreateMap<Direct, DirectViewModel.DirectExpose>();
            CreateMap<DirectUser, DirectViewModel.DirectUserExpose>();
            CreateMap<Direct, DirectViewModel.MyDirect>();
            CreateMap<Direct, string>().ConvertUsing(r => r.Name);

            // CreateMap<DirectUser, DirectViewModel.MyDirect>()
            //     .ForMember(desc => desc.User,
            //         so => so.MapFrom(d => d.dire))
            //     .ForMember(desc => desc.User,
            //         so => so.MapFrom(d => d.DestinationUser));
        }
    }
}