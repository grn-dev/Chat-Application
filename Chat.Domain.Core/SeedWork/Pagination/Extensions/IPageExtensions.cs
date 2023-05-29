using System;
using System.Collections.Generic;
using System.Linq;
using Garnet.Pagination;
using Garnet.Standard.Pagination;

namespace Chat.Domain.Core.SeedWork.Pagination.Extensions
{
    public static class IPageExtensions
    {
        public static IPagedElements<TDestination> Map<TSource, TDestination>(
            this IPagedElements<TSource> sourcePagedElements,
            List<TDestination> content)
            where TDestination : class where TSource : class
        {
            return new PagedElements<TDestination>(sourcePagedElements.Pagination, content,
                sourcePagedElements.NumberOfTotalElements);
        }

        public static IPagedElements<TDestination> Map<TSource, TDestination>(
            this IPagedElements<TSource> sourcePagedElements,
            Func<TSource, TDestination> mapper)
            where TDestination : class where TSource : class
        {
            return new PagedElements<TDestination>(
                sourcePagedElements.Pagination,
                sourcePagedElements.Elements.Select(mapper).ToList(),
                sourcePagedElements.NumberOfTotalElements);
        }
    }
}
