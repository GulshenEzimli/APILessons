using Application.Interfaces.Repositories.EfCore;
using Application.Interfaces.UnitOfWorks;
using Persistence.Context;
using Persistence.Repositories.EfCore;

namespace Persistence.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApiLessonsDbContext _dbcontext;
        public UnitOfWork(ApiLessonsDbContext context)
        {
            _dbcontext = context;
        }

        IBaseRepository<T> IUnitOfWork.BaseRepository<T>() => new BaseRepository<T>(_dbcontext); 

        public async Task<int> SaveAsync() => await _dbcontext.SaveChangesAsync();

        public int Save() => _dbcontext.SaveChanges();

        public async ValueTask DisposeAsync() => await _dbcontext.DisposeAsync();
    }
}
