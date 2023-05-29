using Chat.Domain.Common;
using NetDevPack.Domain;

namespace Chat.Application.Configuration.Data.BasicQuery
{
    public class GetByIdToDestQuery<TDomain, TIdentifier, TDestination> : IQuery<TDestination>
     where TDomain : BaseModel<TIdentifier>, IAggregateRoot

    {
        public TIdentifier Id { get; set; }
    }
 
}
