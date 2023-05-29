using System;
using Chat.Domain.Models;

namespace Chat.Domain.Commands.User
{ 
    public class RegisterUserCommand : BaseCommand<int>
    {
        public Guid UserId { get; set; }
		public string UserName { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string NationalCode { get; set; }
		public string MobileNumber { get; set; }
		public Gender? Gender { get; set; }
		public DateTime? BirthDate { get; set; }
		public string Password { get; set; }
		public DateTime LastActivity { get; set; }
		public ChatUserStatus Status { get; set; }
		public string Email { get; set; }
		public bool IsBanned { get; set; }
  
    }
}


