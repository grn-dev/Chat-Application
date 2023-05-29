using Chat.Domain.Common;
using NetDevPack.Domain;

namespace Chat.Application.Configuration.Data.BasicQuery
{
    
    public class GetByIdQuery<TDomain, TIdentifier> : IQuery<TDomain>
   where TDomain : BaseModel<TIdentifier>, IAggregateRoot

    {
        public int Id { get; set; }
    }
}
