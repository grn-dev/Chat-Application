using NetDevPack.Messaging;
using System;

namespace Chat.Domain.Models.Events
{
    public class DirectRegisteredEvent : Event
    {
        public DirectRegisteredEvent(Guid aggregateId, Func<int> directId, string directName)
        {
            DirectId = directId;
            DirectName = directName;
        }

        public Func<int> DirectId { get; set; }
        public string DirectName { get; set; }
    }
}