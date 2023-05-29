using System;
using System.Threading.Tasks;
using NetDevPack.Domain;
using Chat.Domain.Commands;
using Chat.Domain.Common;

namespace Chat.Application.Configuration.Data.BasicCommand
{
    public class GenericDeleteCommand<TDomain, TIdentifier> : BaseCommand<TIdentifier>
        where TDomain : BaseModel<TIdentifier>, IAggregateRoot
    {
        public Func<Task<TDomain>> DomainInitiator { get; set; }
    }
}