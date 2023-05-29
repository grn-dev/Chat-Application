using System;
using System.Collections.Generic;
using Chat.Domain.Common;
using Chat.Domain.Models.Events;
using NetDevPack.Domain;

namespace Chat.Domain.Models
{
    public class Direct : BaseModel<int>, IAggregateRoot
    {
        private Direct(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
        public virtual ICollection<DirectUser> DirectUsers { get; private set; }
        public virtual ICollection<DirectMessage> DirectMessages { get; private set; }

        public static Direct Create()
        {
            string directName = Guid.NewGuid().ToString();
            var direct = new Direct(directName);

            direct.AddDomainEvent(new DirectRegisteredEvent(Guid.NewGuid(), () => direct.Id, directName));

            return direct;
        }
    }
}