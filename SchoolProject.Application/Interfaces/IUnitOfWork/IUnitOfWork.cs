using SchoolProject.Application.Interfaces.IGenericRepository;

namespace SchoolProject.Application.Interfaces.IUnitOfWork;
public interface IUnitOfWork : IDisposable
{
	IGenericRepository<T> Repository<T>() where T : class;
	Task<int> CompleteAsync(CancellationToken cancellationToken = default);
}
