using Application.Interfaces.Repositories;
using Domain.Common.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Persistence.Repositories
{
	public class ReadRepository<T> : IReadRepository<T> where T : class, IEntity, new()
	{
		private readonly DbContext dbContext;
		public ReadRepository(DbContext context)
		{
			this.dbContext = context;
		}

		private DbSet<T> Table { get => dbContext.Set<T>(); }
		public async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool enableTracking = false)
		{
			IQueryable<T> entities = Table;
			if (!enableTracking) entities.AsNoTracking();

			if (include is not null) entities = include(entities);
			if (predicate is not null) entities = entities.Where(predicate);
			if (orderBy is not null) entities = orderBy(entities);

			return await entities.ToListAsync();
		}

		public async Task<IList<T>> GetAllByPagingAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool enableTracking = false, int currentPage = 1, int pageSize = 3)
		{
			IQueryable<T> entities = Table;
			if (!enableTracking) entities.AsNoTracking();

			if (include is not null) entities = include(entities);
			if (predicate is not null) entities = entities.Where(predicate);
			if (orderBy is not null) entities = orderBy(entities);

			return await entities.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();
		}

		public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, bool enableTracking = false)
		{
			IQueryable<T> entities = Table;

			if (!enableTracking) entities.AsNoTracking();
			if (include is not null) entities = include(entities);

			return await entities.FirstOrDefaultAsync(predicate);
		}

		public IQueryable<T> Find(Expression<Func<T, bool>> predicate, bool enableTracking = false)
		{
			if (!enableTracking) Table.AsNoTracking();

			return Table.Where(predicate);
		}

		public async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null)
		{
			Table.AsNoTracking();

			if (predicate is not null) Table.Where(predicate);

			return await Table.CountAsync();
		}
	}
}
