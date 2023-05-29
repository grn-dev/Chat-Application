using System.Threading.Tasks;
using Chat.Domain.Attributes;
using Chat.Domain.Commands.User;
using Chat.Infra.Data.Application.CommandHandler.RequestHandler;
using Chat.Infra.Data.Context;

namespace Chat.Infra.Data.Application.CommandHandler.User
{
    [Bean]
    public class RegisterUserCommandHandler : MyCreateRequestHandler<
        RegisterUserCommand, Chat.Domain.Models.User.User, int>
    {
        public RegisterUserCommandHandler(ChatContext context) : base(context)
        {
        }


        protected override Task<Chat.Domain.Models.User.User> InstantiateDomain(RegisterUserCommand request)
        {
            return Task.FromResult(Chat.Domain.Models.User.User.Create(
                request.UserId,
                request.UserName,
                request.FirstName,
                request.LastName,
                request.NationalCode,
                request.MobileNumber,
                request.Gender,
                request.BirthDate,
                request.Password,
                request.LastActivity,
                request.Status,
                request.Email,
                request.IsBanned));
        }
    }
}