using Application.Interfaces.Repositories;
using Domain.Common.Interface;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Persistence.Repositories
{
	public class WriteRepository<T> : IWriteRepository<T> where T :class, IEntity, new()
	{
		private readonly DbContext dbContext;
		public WriteRepository(DbContext context)
		{
			dbContext = context;
		}

		private DbSet<T> Table {  get => dbContext.Set<T>(); }
		public async Task AddAsync(T entity)
		{
			await Table.AddAsync(entity);			
		}

		public async Task AddRangeAsync(IList<T> entities)
		{
			await Table.AddRangeAsync(entities);
		}

		public async Task<T> UpdateAsync(T entity)
		{
			await Task.Run(() => Table.Update(entity));
			return entity;
		}

		public async Task DeleteAsync(T entity)
		{
			await Task.Run(() => Table.Remove(entity));
		}
	}
}
