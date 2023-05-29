using Chat.Domain.Common;
using NetDevPack.Domain;
using System.Collections.Generic;

namespace Chat.Domain.Models.Ticket
{
    public class TicketCategory : BaseModel<int>, IAggregateRoot
    {
        private TicketCategory(string description,
                    string name
)
        {
            Description = description;
            Name = name;

        }
        private TicketCategory() { }
        public string Description { get; private set; }
        public string Name { get; private set; }

        public ICollection<Ticket> Tickets { get; private set; }


        public static TicketCategory CreateRegistered(string description,
                    string name
)
        {
            return new TicketCategory(description, name);
        }
        public void Update(string description,
                    string name
)
        {
            Description = description;
            Name = name;

        }
        public static TicketCategory DeleteRegistered(int id) => new TicketCategory() { Id = id };
    }
}

