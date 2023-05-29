using System;
using System.Linq.Expressions;
using Chat.Domain.Common;
using NetDevPack.Domain;

namespace Chat.Application.Configuration.Data.BasicQuery
{
    public class AnyByPredicateQuery<TDomain, TIdentifier> : IQuery<bool>
        where TDomain : BaseModel<TIdentifier>, IAggregateRoot
    {
        public Expression<Func<TDomain, bool>> Predicate { get; set; }
    }
}