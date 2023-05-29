using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks; 
using Garnet.Standard.Pagination;
using NetDevPack.Domain;

namespace Chat.Domain.Repositorys
{
    public interface IRepository<TEntity> : NetDevPack.Data.IRepository<TEntity> where TEntity : class, IAggregateRoot
    {
        TEntity Get(int id);

        ValueTask<TEntity> GetAsync(int id);
        ValueTask<TEntity> GetAsync(object id);

        IEnumerable<TEntity> GetAll();

        IPagedElements<TEntity> GetAllUsePageable(IPagination pageable);

        Task<IPagedElements<TEntity>> GetAllUsePageableAsync(IPagination pageable);

        Task<List<TEntity>> GetAllAsync();

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);

        Task AddAsync(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);

        Task AddRangeAsync(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entities);

        void Update(TEntity entity);
        Task<int> Command(string command);
        void Track(TEntity entity);


    }
}