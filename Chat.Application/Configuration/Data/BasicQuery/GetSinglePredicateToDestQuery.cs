using Chat.Domain.Common;
using NetDevPack.Domain;
using System;
using System.Linq.Expressions;

namespace Chat.Application.Configuration.Data.BasicQuery
{
    public class GetSinglePredicateToDestQuery<TDomain, TIdentifier, TDestination> : IQuery<TDestination>
     where TDomain : BaseModel<TIdentifier>, IAggregateRoot

    {
        public Expression<Func<TDomain, bool>> Predicate { get; set; }
    }
 
}
