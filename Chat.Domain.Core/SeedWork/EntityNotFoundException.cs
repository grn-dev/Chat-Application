using System;

namespace Chat.Domain.Core.SeedWork
{
    public class EntityNotFoundException : Exception
    {
        public string Details { get; }
        public string ErroCode { get; }
        public string Entity { get; }

        public EntityNotFoundException(string entity) : base($"Entity {entity} Not Found")
        {
         
            Details = entity + " Entity Not Found";
            ErroCode = "Entity_Not_Found";
            Entity = entity;
        }
    }
}