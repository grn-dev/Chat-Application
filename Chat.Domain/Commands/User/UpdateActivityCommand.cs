using System;

namespace Chat.Domain.Commands.User
{
    public class UpdateActivityCommand : BaseCommand<int>
    {
        public Guid UserId { get; set; }
    }
}