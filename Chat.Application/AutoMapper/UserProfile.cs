using AutoMapper;
using Chat.Application.ViewModels;
using Chat.Domain.Commands.User;
using Chat.Domain.Models.User;

namespace Chat.Application.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserViewModel.UserBaseExpose>();
            CreateMap<User, UserViewModel.UserExpose>();
            CreateMap<UserViewModel.RegisterUser, RegisterUserCommand>();
            CreateMap<UserViewModel.UpdateUser, UpdateUserCommand>();
        }
    }
}