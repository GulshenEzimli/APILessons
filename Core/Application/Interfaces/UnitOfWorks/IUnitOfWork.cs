using Application.Interfaces.Repositories;
using Domain.Common.Interface;

namespace Application.Interfaces.UnitOfWorks
{
	public interface IUnitOfWork : IAsyncDisposable
	{
		IReadRepository<T> GetReadRepository<T>() where T : class, IEntity, new();
		IWriteRepository<T> GetWriteRepository<T>() where T : class, IEntity, new();
		Task<int> SaveAsync();
		int Save();
	}
}
