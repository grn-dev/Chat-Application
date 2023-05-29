namespace Chat.Domain.Commands.ChannelAdmin
{ 
    public class RegisterChannelAdminCommand : BaseCommand<int>
    {
        public int ChatUserId { get; set; }
		public int ChannelId { get; set; }
  
    }
}


