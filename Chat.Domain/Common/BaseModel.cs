using System;

namespace Chat.Domain.Common
{
    public class BaseModel<TPrimaryKey> : MyEntity
    {
        public TPrimaryKey Id { get; set; }
        public DateTime CreateDate { get; set; }
        //public DateTime? ModifyDate { get; set; }
        public Guid CreatorUserId { get; set; }
    }
}
