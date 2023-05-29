namespace Chat.Domain.Commands.Direct
{
    public class RegisterDirectUserCommand : BaseCommand<int>
    {
        public int UserId { get; set; }
        public int DirectId { get; set; }
    }
}