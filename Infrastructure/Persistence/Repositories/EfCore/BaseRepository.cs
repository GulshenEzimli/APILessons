using Application.Interfaces.Repositories.EfCore;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Persistence.Context;
using System.Linq.Expressions;

namespace Persistence.Repositories.EfCore
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IEntityBase, new()
    {
        private readonly ApiLessonsDbContext _dbContext;
        private DbSet<T> DbSet { get; }
        public BaseRepository(ApiLessonsDbContext dbContext)
        {
            _dbContext = dbContext;
            DbSet = _dbContext.Set<T>();
        }

        public async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool enableTracking = false)
        {
            IQueryable<T> queryable = DbSet.AsQueryable();

            if (!enableTracking)
                queryable = queryable.AsNoTracking();

            if (predicate is not null)
                queryable = queryable.Where(predicate);

            if (include is not null)
                queryable = include(queryable);

            if (orderBy is not null)
                queryable = orderBy(queryable);

            return await queryable.ToListAsync();
        }

        public async Task<IList<T>> GetAllByPagingAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool enableTracking = false, int currentPage = 1, int pageSize = 5)
        {
            IQueryable<T> queryable = DbSet.AsQueryable();

            if (!enableTracking)
                queryable = queryable.AsNoTracking();

            if (predicate is not null)
                queryable = queryable.Where(predicate);

            if (include is not null)
                queryable = include(queryable);

            if (orderBy is not null)
                queryable = orderBy(queryable); ;

            return await queryable.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool enableTracking = false)
        {
            IQueryable<T> queryable = DbSet.AsQueryable();

            if (!enableTracking)
                queryable = queryable.AsNoTracking();

            if (include is not null)
                queryable = include(queryable);

            if (orderBy is not null)
                queryable = orderBy(queryable); ;

            return await queryable.FirstOrDefaultAsync(predicate);
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate, bool enableTracking)
        {
            IQueryable<T> queryable = DbSet.AsQueryable();

            if (!enableTracking)
                queryable = queryable.AsNoTracking();

            return queryable.Where(predicate);
        }

        public async Task CreateAsync(T entity)
        {
            await DbSet.AddAsync(entity);
        }

        public async Task CreateRangeAsync(IList<T> entities)
        {
            await DbSet.AddRangeAsync(entities);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            await Task.Run(() => DbSet.Update(entity));
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            await Task.Run(() => DbSet.Remove(entity));
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null)
        {
            IQueryable<T> queryable = DbSet.AsQueryable().AsNoTracking();

            if (predicate is not null)
                queryable = queryable.Where(predicate);

            return await queryable.CountAsync();
        }

    }
}
