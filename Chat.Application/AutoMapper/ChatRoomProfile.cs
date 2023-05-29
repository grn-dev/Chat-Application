using AutoMapper; 
using Chat.Domain.Models;
using static Chat.Application.ViewModels.ChatRoomViewModel;

namespace Chat.Application.AutoMapper
{
    public class ChatRoomProfile : Profile
    {
        public ChatRoomProfile()
        {
            CreateMap<ChatRoom, ChatRoomExpose>();
            CreateMap<ChatRoom, ChatRoomBaseExpose>(); 
            CreateMap<HubCreatRoomClientToServer, CreatRoomRegister>();
            CreateMap<HubJoinRoomClientToServer, JoinRoomRegister>();
            CreateMap<ChatRoom, string>().ConvertUsing(r => r.Name);
            //CreateMap<JoinTicketRegister, JoinRoomRegister>();
        }
    }
} 