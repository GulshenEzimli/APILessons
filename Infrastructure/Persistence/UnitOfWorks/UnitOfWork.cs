using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWorks;
using Domain.Common.Interface;
using Persistence.Contexts;
using Persistence.Repositories;

namespace Persistence.UnitOfWorks
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApiContext dbContext;
		public UnitOfWork(ApiContext context)
		{
			dbContext = context;
		}
		IReadRepository<T> IUnitOfWork.GetReadRepository<T>()
		{
			return new ReadRepository<T>(dbContext);
		}

		IWriteRepository<T> IUnitOfWork.GetWriteRepository<T>()
		{
			return new WriteRepository<T>(dbContext);
		}

		public async Task<int> SaveAsync()
		{
			return await dbContext.SaveChangesAsync();
		}

		public int Save()
		{
			return dbContext.SaveChanges();
		}

		public async ValueTask DisposeAsync()
		{
			 await dbContext.DisposeAsync();
		}
	}
}
