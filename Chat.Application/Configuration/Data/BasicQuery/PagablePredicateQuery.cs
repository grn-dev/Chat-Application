using Chat.Domain.Common;
using Chat.Domain.Core.SeedWork.Pagination;
using NetDevPack.Domain;
using System;
using System.Linq.Expressions;
using Garnet.Standard.Pagination;

namespace Chat.Application.Configuration.Data.BasicQuery
{
    public class PagablePredicateQuery<TDomain, TIdentifier, TDestination> : IQuery<IPagedElements<TDestination>>
       where TDomain : BaseModel<TIdentifier>, IAggregateRoot
       where TDestination : class

    {
        public IPagination Pagination { get; set; }
        public Expression<Func<TDomain, bool>> Predicate { get; set; }
    }
}
