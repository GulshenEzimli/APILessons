using Application.Interfaces.Repositories.EfCore;
using Domain.Common;
using Domain.Interfaces;

namespace Application.Interfaces.UnitOfWorks
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IBaseRepository<T> BaseRepository<T>() where T : class, IEntityBase, new();
        Task<int> SaveAsync();
        int Save();
    }
}
