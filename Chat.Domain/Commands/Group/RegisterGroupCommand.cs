namespace Chat.Domain.Commands.Group
{
    public class RegisterGroupCommand : BaseCommandWithDomain<Models.Group>
    {
        public bool IsPrivate { get; set; }
        public string Topic { get; set; }
        public string InviteCode { get; set; }
    }
}