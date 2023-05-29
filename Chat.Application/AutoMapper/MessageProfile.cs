using AutoMapper;
using Chat.Application.ViewModels;
using Chat.Domain.Commands.Message;
using Chat.Domain.Models;

namespace Chat.Application.AutoMapper
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<Message, MessageViewModel.MessageExpose>();
            CreateMap<MessageViewModel.RegisterMessage, RegisterMessageCommand>();

            CreateMap<ChatRoomMessage, MessageViewModel.MessageChatRoom>()
                .ForMember(desc => desc.Content,
                    so => so.MapFrom(d => d.Message.Content))
                .ForMember(desc => desc.UserName,
                    so => so.MapFrom(d => d.Sender.UserName))
                .ForMember(desc => desc.MessageId,
                    so => so.MapFrom(d => d.MessageId))
                .ForMember(desc => desc.ParentId,
                    so => so.MapFrom(d => d.Message.ParentId))
                .ForMember(desc => desc.Attachment,
                    so => so.MapFrom(d => d.Message.Attachment))
                .ForMember(desc => desc.CreateDate,
                    so => so.MapFrom(d => d.CreateDate));

            CreateMap<DirectMessage, MessageViewModel.MessageDirect>()
                .ForMember(desc => desc.Content,
                    so => so.MapFrom(d => d.Message.Content))
                .ForMember(desc => desc.UserName,
                    so => so.MapFrom(d => d.Sender.UserName))
                .ForMember(desc => desc.ParentId,
                    so => so.MapFrom(d => d.Message.ParentId))
                .ForMember(desc => desc.Attachment,
                    so => so.MapFrom(d => d.Message.Attachment))
                .ForMember(desc => desc.CreateDate,
                    so => so.MapFrom(d => d.CreateDate));


            CreateMap<Message, MessageViewModel.MessageIncludeUserRoom>();
            CreateMap<MessageViewModel.UpdateMessage, UpdateMessageCommand>();
        }
    }
}