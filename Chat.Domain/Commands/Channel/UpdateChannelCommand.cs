using System;
using Chat.Domain.Models;

namespace Chat.Domain.Commands.Channel
{
     
    public class UpdateChannelCommand : BaseCommand<int>
    { 
	    public bool IsPrivate { get; set; }
	    public string Topic { get; set; }
	    public string InviteCode { get; set; }
 
    }
}

