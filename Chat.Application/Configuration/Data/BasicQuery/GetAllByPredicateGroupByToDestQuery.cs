using Chat.Domain.Common;
using NetDevPack.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Chat.Application.Configuration.Data.BasicQuery
{
    public class GetAllByPredicateToDestQuery<TDomain, TIdentifier, TDestination> : IQuery<List<TDestination>>
     where TDomain : BaseModel<TIdentifier>, IAggregateRoot

    {
        public Expression<Func<TDomain, bool>> Predicate { get; set; }
    }

    public class GetAllByPredicateGroupByToDestQuery<TDomain, TIdentifier, TDestination, TKey> : IQuery<List<TDestination>>
     where TDomain : BaseModel<TIdentifier>, IAggregateRoot

    {
        public Expression<Func<TDomain, bool>> Predicate { get; set; }
        public Expression<Func<TDomain, TKey>> keySelector { get; set; }
    }

}
