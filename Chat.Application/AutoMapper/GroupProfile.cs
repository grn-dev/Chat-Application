using AutoMapper;
using Chat.Application.ViewModels;
using Chat.Domain.Commands.Group;
using Chat.Domain.Models;

namespace Chat.Application.AutoMapper
{
    public class GroupProfile : Profile
    {
        public GroupProfile()
        {
            CreateMap<Group, GroupViewModel.GroupExpose>()
                .ForMember(desc => desc.Name,
                    so => so.MapFrom(d => d.ChatRoom.Name))
                .ForMember(desc => desc.Topic,
                    so => so.MapFrom(d => d.ChatRoom.Topic));

            CreateMap<Group, GroupViewModel.MyGroup>()
                .ForMember(desc => desc.Name,
                    so => so.MapFrom(d => d.ChatRoom.Name))
                .ForMember(desc => desc.Topic,
                    so => so.MapFrom(d => d.ChatRoom.Topic))
                .ForMember(desc => desc.Closed,
                    so => so.MapFrom(d => d.ChatRoom.Closed));

            CreateMap<Group, GroupViewModel.GroupInfo>()
                .ForMember(desc => desc.Name,
                    so => so.MapFrom(d => d.ChatRoom.Name))
                .ForMember(desc => desc.Topic,
                    so => so.MapFrom(d => d.ChatRoom.Topic));

            CreateMap<GroupViewModel.RegisterGroup, RegisterGroupCommand>();
            CreateMap<GroupViewModel.UpdateGroup, UpdateGroupCommand>();
        }
    }
}