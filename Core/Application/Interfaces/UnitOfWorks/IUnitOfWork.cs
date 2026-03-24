using Application.Interfaces.Repositories.EfCore;
using Domain.Common;
using Domain.Interfaces;

namespace Application.Interfaces.UnitOfWorks
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IBaseRepository<T> BaseRepository<T>() where T : class, IEntityBase, new();
        Task<int> SaveAsync(CancellationToken cancellationToken = default);
        int Save();

        Task BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);
        Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
        Task DisposeTransactionAsync();
    }
}
