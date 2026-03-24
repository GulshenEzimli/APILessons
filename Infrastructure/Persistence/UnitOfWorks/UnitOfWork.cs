using Application.Interfaces.Repositories.EfCore;
using Application.Interfaces.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Persistence.Context;
using Persistence.Repositories.EfCore;

namespace Persistence.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApiLessonsDbContext _dbcontext;
        private IDbContextTransaction? _dbTransaction;
        private bool _disposed;
        public UnitOfWork(ApiLessonsDbContext context)
        {
            _dbcontext = context;
            _disposed = true;
        }

        #region Private
        private async Task CreateTransactionIfNotExistsAsync(CancellationToken cancellationToken = default)
        {
            if (_dbTransaction == null)
            {
                _dbTransaction = await _dbcontext.Database.BeginTransactionAsync(cancellationToken);
                _disposed = false;
            }
        }
        #endregion

        #region public methods
        IBaseRepository<T> IUnitOfWork.BaseRepository<T>()
        {
            return new BaseRepository<T>(_dbcontext);
        }

        public async Task<int> SaveAsync(CancellationToken cancellationToken = default)
        {
            return await _dbcontext.SaveChangesAsync(cancellationToken);
        }

        public int Save()
        {
            return _dbcontext.SaveChanges();
        }

       
        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            await CreateTransactionIfNotExistsAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_dbTransaction == null)
                throw new InvalidOperationException("Aktiv tranzaksiya tapilmadi");

            await _dbTransaction.CommitAsync(cancellationToken);
        }

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_dbTransaction == null)
                throw new InvalidOperationException("Aktiv tranzaksiya tapilmadi");

            await _dbTransaction.RollbackAsync(cancellationToken);
        }

        public async Task DisposeTransactionAsync()
        {
            if (_dbTransaction != null && !_disposed)
            {
                await _dbTransaction.DisposeAsync();
                _dbTransaction = null;
            }
            _disposed = true;
        }

        public async ValueTask DisposeAsync()
        {
            await DisposeTransactionAsync();
            await _dbcontext.DisposeAsync();
        }

        #endregion
    }
}
