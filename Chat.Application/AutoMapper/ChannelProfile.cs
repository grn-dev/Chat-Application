using AutoMapper;
using Chat.Application.ViewModels;
using Chat.Domain.Commands.Channel;

namespace Chat.Application.AutoMapper
{
    public class ChannelProfile : Profile
    {
        public ChannelProfile()
        {
            CreateMap<Domain.Models.Channel, ChannelViewModel.ChannelExpose>();
            CreateMap<Domain.Models.Channel, ChannelViewModel.ChannelInfo>();
            CreateMap<Domain.Models.Channel, ChannelViewModel.MyChannel>() 
                .ForMember(desc => desc.Name,
                so => so.MapFrom(d => d.ChatRoom.Name))
                .ForMember(desc => desc.Topic,
                    so => so.MapFrom(d => d.ChatRoom.Topic))
                .ForMember(desc => desc.Closed,
                so => so.MapFrom(d => d.ChatRoom.Closed));
            CreateMap<ChannelViewModel.RegisterChannel, RegisterChannelCommand>();
            CreateMap<ChannelViewModel.UpdateChannel, UpdateChannelCommand>();
        }
    }
}