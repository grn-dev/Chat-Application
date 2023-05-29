using System.Threading.Tasks;
using Chat.Domain.Attributes;
using Chat.Domain.Commands.User;
using Chat.Infra.Data.Application.QueryHandler.RequestHandler;
using Chat.Infra.Data.Context;

namespace Chat.Infra.Data.Application.CommandHandler.User
{
    [Bean]
    public class UpdateUserCommandHandler : MyUpdateRequestHandler<
        UpdateUserCommand, Chat.Domain.Models.User.User, int>
    {
        public UpdateUserCommandHandler(ChatContext context) : base(context)
        {
        }


        protected override Task UpdateFields(Chat.Domain.Models.User.User domain, UpdateUserCommand request)
        {
            domain.Update(request.UserId,
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
                request.IsBanned);
            return Task.FromResult(0);
        }
    }
}