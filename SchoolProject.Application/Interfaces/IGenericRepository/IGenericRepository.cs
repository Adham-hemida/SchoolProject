using System.Linq.Expressions;

namespace SchoolProject.Application.Interfaces.IGenericRepository;
public interface IGenericRepository<T> where T : class
{
	IQueryable<T> GetAsQueryable();
	public Task<T?> GetByIdAsync(object id, CancellationToken cancellationToken = default);
	public Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
	public Task<T> FindAsync(Expression<Func<T, bool>> criteria, Func<IQueryable<T>, IQueryable<T>>? includes = null, CancellationToken cancellationToken = default);
	public Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, Func<IQueryable<T>, IQueryable<T>>? includes = null, CancellationToken cancellationToken = default);

	//public Task<IEnumerable<TDestination>> FindAllProjectedAsync<TDestination>(
	//	Expression<Func<T, bool>>? criteria = null,
	//CancellationToken cancellationToken = default);

	public Task<IEnumerable<TDestination>> FindAllProjectedWithSelect<TDestination>(
	Expression<Func<T, TDestination>> selector,
	Expression<Func<T, bool>>? criteria = null,
	CancellationToken cancellationToken = default);

	public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default); 
	public Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default);
	public T UpdateAsync(T entity);
	public void Delete(T entity);

}
