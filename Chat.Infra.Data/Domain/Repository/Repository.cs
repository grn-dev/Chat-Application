using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Chat.Infra.Data.Domain.Pagination;
using Garnet.Standard.Pagination;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using NetDevPack.Domain;

namespace Chat.Infra.Data.Domain.Repository
{
    public class Repository<TEntity, TContext> : Chat.Domain.Repositorys.IRepository<TEntity>
          where TEntity : class, IAggregateRoot
          where TContext : DbContext, IUnitOfWork
    {
        protected readonly TContext Db;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(TContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        public void Dispose()
        {
            Db.Dispose();
        }

        public IUnitOfWork UnitOfWork => Db;

        public TEntity Get(int id)
        {
            return DbSet.Find(id);
        }

        public ValueTask<TEntity> GetAsync(int id)
        {
            return DbSet.FindAsync(id);
        }
        public ValueTask<TEntity> GetAsync(object id)
        {
            return DbSet.FindAsync(id);
        }
        public IEnumerable<TEntity> GetAll()
        {
            return DbSet.ToList();
        }

        public IPagedElements<TEntity> GetAllUsePageable(IPagination pagination)
        {
            return DbSet.UsePageable(pagination);
        }

        public Task<IPagedElements<TEntity>> GetAllUsePageableAsync(IPagination pagination)
        {
            return DbSet.UsePageableAsync(pagination);
        }

        public Task<List<TEntity>> GetAllAsync()
        {
            return DbSet.AsQueryable().ToListAsync();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate).ToListAsync();
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.SingleOrDefault(predicate);
        }

        public Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.SingleOrDefaultAsync(predicate);
        }

        public void Add(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public Task AddAsync(TEntity entity)
        {
            return DbSet.AddAsync(entity).AsTask();
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            DbSet.AddRange(entities);
        }

        public Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            return DbSet.AddRangeAsync(entities);
        }

        public void Remove(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            DbSet.RemoveRange(entities);
        }

        public void Update(TEntity entity)
        {
            DbSet.Update(entity);
        }

        public void Track(TEntity entity)
        {
            if (Db.ChangeTracker.Entries<TEntity>().Any(entry => entry.Entity == entity))
            {
                return;
            }

            var dbEntry = Db.Entry(entity);
            dbEntry.State = EntityState.Unchanged;
        }

        public Task<int> Command(string command)
        {
            return Db.Database.ExecuteSqlRawAsync(command);
        }
    }
}