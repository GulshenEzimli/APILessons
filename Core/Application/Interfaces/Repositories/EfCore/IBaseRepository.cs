using Domain.Interfaces;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Interfaces.Repositories.EfCore
{
    public interface IBaseRepository<T> where T : class, IEntityBase, new() 
    {
        Task<IList<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, 
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, 
            bool enableTracking = false);
        Task<IList<T>> GetAllByPagingAsync(Expression<Func<T, bool>>? predicate = null, 
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            bool enableTracking = false, int currentPage = 1, int pageSize = 5);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate, 
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            bool enableTracking = false);

        IQueryable<T> Find(Expression<Func<T, bool>> predicate, bool enableTracking = false);
        Task<int> CreateAsync(T entity);
        Task<int> CreateRangeAsync(IList<T> entities);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null);
       
    }
}
