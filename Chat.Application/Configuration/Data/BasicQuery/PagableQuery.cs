using Garnet.Standard.Pagination;
using Chat.Domain.Common;
using Chat.Domain.Core.SeedWork.Pagination;
using NetDevPack.Domain;

namespace Chat.Application.Configuration.Data.BasicQuery
{
    public class PagableQuery<TDomain, TIdentifier, TDestination> : IQuery<IPagedElements<TDestination>>
       where TDomain : BaseModel<TIdentifier>, IAggregateRoot
       where TDestination : class

    {
        public IPagination Pagination { get; set; }
    }
}
