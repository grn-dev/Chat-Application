using System;

namespace Chat.Domain.Commands.Direct
{
    public class UpdateDirectCommand : BaseCommand<int>
    {
        public string Topic { get; set; }
    }
}