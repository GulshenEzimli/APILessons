using Domain.Common.Interface;

namespace Application.Interfaces.Repositories
{
	public interface IWriteRepository<T> where T: class, IEntity, new()
	{
		Task AddAsync(T entity);
		Task AddRangeAsync(IList<T> entities);
		Task<T> UpdateAsync(T entity);
		Task DeleteAsync(T entity);
	}
}
