using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetDevPack.Domain;
using Chat.Domain.Commands;
using Chat.Domain.Common;

namespace Chat.Application.Configuration.Data.BasicCommand
{
    public class GenericDeleteBatchCommand<TDomain, TIdentifier> : BaseBatchCommand<TIdentifier>
        where TDomain : BaseModel<TIdentifier>, IAggregateRoot
    {
        public Func<Task<List<TDomain>>> DomainInitiator { get; set; }
    }
}