using Mapster;
using SchoolProject.Application.Abstractions;
using SchoolProject.Application.Contracts.Department;
using SchoolProject.Application.Contracts.Student;
using SchoolProject.Application.ErrorHandler;
using SchoolProject.Application.Interfaces.IServices;
using SchoolProject.Application.Interfaces.IUnitOfWork;
using SchoolProject.Domain.Entites;
using SchoolProject.Infrastructure.Data;

namespace SchoolProject.Infrastructure.Implementation.Services;
public class DepartmentService(IUnitOfWork unitOfWork, ApplicationDbContext context) : IDepartmentService
{
	private readonly IUnitOfWork _unitOfWork = unitOfWork;
	private readonly ApplicationDbContext _context = context;

	public async Task<Result<IEnumerable<DepartmentResponse>>> GetAllAsync( CancellationToken cancellationToken = default)
	{
		var result = await _unitOfWork.Repository<Department>()
			.FindAllProjectedAsync<DepartmentResponse>(cancellationToken: cancellationToken);

		return Result.Success(result);
	}

	public async Task<Result<DepartmentResponse>> GetByIdAsync(int id, CancellationToken cancellationToken = default!)
	{
		var department = await _unitOfWork.Repository<Department>().FindAsync(x => x.Id == id, null, cancellationToken);

		if (department is null)
			return Result.Failure<DepartmentResponse>(DepartmentErrors.DepartmentNotFound);

		return Result.Success(department.Adapt<DepartmentResponse>());
	}	
	
	
	public async Task<Result<DepartmentResponse>> AddAsync(DepartmentRequest request, CancellationToken cancellationToken = default)
	{
		var departmentIsExists =await  _unitOfWork.Repository<Department>()
			.AnyAsync(x => x.Name.ToLower() == request.Name.ToLower().Trim(), cancellationToken: cancellationToken);

		if (departmentIsExists)
			return Result.Failure<DepartmentResponse>(DepartmentErrors.DuplicatedDepartment);

		var department=request.Adapt<Department>();

		await _unitOfWork.Repository<Department>().CreateAsync(department);
		await _unitOfWork.CompleteAsync(cancellationToken);

		return Result.Success(department.Adapt<DepartmentResponse>());
	}

	public async Task<Result> UpdateAsync(int id, DepartmentRequest request, CancellationToken cancellationToken = default)
	{
		var department=await _unitOfWork.Repository<Department>()
			.GetByIdAsync(id,cancellationToken);

		if (department is null)
			return Result.Failure(DepartmentErrors.DepartmentNotFound);

		var departmentIsExists = await _unitOfWork.Repository<Department>().AnyAsync(x => x.Name == request.Name && x.Id != id, cancellationToken);

		if (departmentIsExists)
			return Result.Failure(DepartmentErrors.DuplicatedDepartment);

		department=request.Adapt(department);
		_unitOfWork.Repository<Department>().Update(department);
		await _unitOfWork.CompleteAsync(cancellationToken);
		return Result.Success();
	}


	public async Task<Result> ToggleStatusAsync(int  id, CancellationToken cancellationToken = default)
	{
		var departmet = await _unitOfWork.Repository<Department>().GetByIdAsync(id, cancellationToken);

		if (departmet is null)
			return Result.Failure(StudentErrors.StudentNotFound);

		departmet.IsActive = !departmet.IsActive;

		_unitOfWork.Repository<Department>().Update(departmet);
		await _unitOfWork.CompleteAsync(cancellationToken);
		return Result.Success();
	}

}
