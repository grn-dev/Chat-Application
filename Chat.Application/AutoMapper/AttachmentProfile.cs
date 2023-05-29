using AutoMapper;
using Chat.Application.ViewModels;
using Chat.Domain.Commands.Attachment;
using Chat.Domain.Models;

namespace Chat.Application.AutoMapper
{
    public class AttachmentProfile : Profile
    {
        public AttachmentProfile()
        {
            CreateMap<AttachmentViewModel.RegisterAttachmentToDirect, RegisterAttachmentCommand>();
            CreateMap<AttachmentViewModel.RegisterAttachmentToRoom, RegisterAttachmentCommand>();
            CreateMap<Attachment, AttachmentViewModel.AttachmentExpose>();
            CreateMap<Attachment, AttachmentViewModel.AttachmentBaseExpose>();
        }
    }
}