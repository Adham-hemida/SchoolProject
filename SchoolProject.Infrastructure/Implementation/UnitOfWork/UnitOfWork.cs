using SchoolProject.Application.Interfaces.IGenericRepository;
using SchoolProject.Application.Interfaces.IUnitOfWork;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.Implementation.GenericRepository;

namespace SchoolProject.Infrastructure.Implementation.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
	private readonly ApplicationDbContext _context;
	private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();
	public UnitOfWork(ApplicationDbContext context)
	{
		_context = context;
	}

	public IGenericRepository<T> Repository<T>() where T : class
	{
		if (_repositories.ContainsKey(typeof(T)))
		{
			return (IGenericRepository<T>)_repositories[typeof(T)];
		}
		var repository = new GenericRepository<T>(_context);
		_repositories.Add(typeof(T), repository);
		return repository;
	}

	public async Task<int> CompleteAsync(CancellationToken cancellationToken = default)
	{
		return await _context.SaveChangesAsync(cancellationToken);
	}

	public void Dispose()
	{
		_context.Dispose();
	}
}
