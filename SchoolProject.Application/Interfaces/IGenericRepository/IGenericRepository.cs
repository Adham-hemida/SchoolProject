using System.Linq.Expressions;

namespace SchoolProject.Application.Interfaces.IGenericRepository;
public interface IGenericRepository<T> where T : class
{
	IQueryable<T> GetAsQueryable();
	 Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
	 Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
	 Task<T> FindAsync(Expression<Func<T, bool>> criteria, Func<IQueryable<T>, IQueryable<T>>? includes = null, CancellationToken cancellationToken = default);
	 Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, Func<IQueryable<T>, IQueryable<T>>? includes = null, CancellationToken cancellationToken = default);

	 Task<IEnumerable<TDestination>> FindAllProjectedAsync<TDestination>(
		Expression<Func<T, bool>>? criteria = null,
	CancellationToken cancellationToken = default);

	 Task<IEnumerable<TDestination>> FindAllProjectedWithSelect<TDestination>(
	Expression<Func<T, TDestination>> selector,
	Expression<Func<T, bool>>? criteria = null,
	CancellationToken cancellationToken = default);

	 Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default); 
	 Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default);
	 void Update(T entity);
	 void Delete(T entity);
	void DeleteRange(IEnumerable<T> entities);
}
