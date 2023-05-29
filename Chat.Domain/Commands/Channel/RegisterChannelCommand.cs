namespace Chat.Domain.Commands.Channel
{
    public class RegisterChannelCommand : BaseCommandWithDomain<Models.Channel>
    { 
        public string Topic { get; set; }
        public string InviteCode { get; set; }
        public bool IsPrivate { get; set; }
    }
}