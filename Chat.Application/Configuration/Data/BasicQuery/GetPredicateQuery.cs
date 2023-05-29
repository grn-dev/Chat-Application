using System;
using System.Linq.Expressions;
using Chat.Domain.Common;
using NetDevPack.Domain;

namespace Chat.Application.Configuration.Data.BasicQuery
{
    public class GetPredicateQuery<TDomain, TIdentifier> : IQuery<TDomain>
     where TDomain : BaseModel<TIdentifier>, IAggregateRoot

    {
        public Expression<Func<TDomain, bool>> Predicate { get; set; }
    }
}