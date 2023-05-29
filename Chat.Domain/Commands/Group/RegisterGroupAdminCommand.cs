namespace Chat.Domain.Commands.GroupAdmin
{ 
    public class RegisterGroupAdminCommand : BaseCommand<int>
    {
        public int UserId { get; set; }
		public int GroupId { get; set; }
  
    }
}


