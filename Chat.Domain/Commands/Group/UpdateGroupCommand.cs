namespace Chat.Domain.Commands.Group
{
    public class UpdateGroupCommand : BaseCommand<int>
    {
        public bool IsPrivate { get; set; }
        public string Topic { get; set; }
        public string InviteCode { get; set; }
    }
}