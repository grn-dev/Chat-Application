using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using Garnet.Detail.Pagination.ListExtensions.Extensions;
using Garnet.Standard.Pagination;

namespace Chat.Infra.Data.Domain.Pagination
{
    public static class QueryableExtensions
    {
        public static MapperConfiguration _mapperConfiguration;
        static QueryableExtensions()
        {
          //   var profiles = Assembly.Load("Chat.Application")
          // .GetTypes()
          // .Where(t => typeof(Profile).GetTypeInfo().IsAssignableFrom(t.GetTypeInfo()))
          // .Where(t => !t.GetTypeInfo().IsAbstract)
          // .ToList();
          //
          //   _mapperConfiguration = new MapperConfiguration(mc =>
          //   {
          //       profiles.ForEach(c => mc.AddProfile(c));
          //
          //   });

        }
        public static IQueryable<T> ToDestination<T>(this IQueryable queryable)
        {
            return queryable.ProjectTo<T>(_mapperConfiguration);
        }

        public static IPagedElements<TEntity> UsePageable<TEntity>(this IQueryable<TEntity> receiver, IPagination pagination)
            where TEntity : class
        {
            return receiver.ToPagedResult(pagination);
        }

        public static Task<IPagedElements<TEntity>> UsePageableAsync<TEntity>(this IQueryable<TEntity> receiver, IPagination pagination)
          where TEntity : class
        {
            return receiver.ToPagedResultAsync(pagination);
        }

        public static Task<IPagedElements<TDestination>> UsePageableUseMapperAsync<TEntity, TDestination>(this IQueryable<TEntity> receiver, IPagination pagination)
         where TEntity : class
         where TDestination : class
        {
            var receiverDestination = receiver.ProjectTo<TDestination>(_mapperConfiguration);

            return receiverDestination.ToPagedResultAsync(pagination);
        }
    }
}
