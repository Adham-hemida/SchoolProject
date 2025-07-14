using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using SchoolProject.Application.Abstractions;
using SchoolProject.Application.Contracts.Student;
using SchoolProject.Application.ErrorHandler;
using SchoolProject.Application.Interfaces.IServices;
using SchoolProject.Application.Interfaces.IUnitOfWork;
using SchoolProject.Domain.Entites;
using SchoolProject.Infrastructure.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace SchoolProject.Infrastructure.Implementation.Services;
public class StudentService(IUnitOfWork unitOfWork, ApplicationDbContext context) : IStudentService
{
	private readonly IUnitOfWork _unitOfWork = unitOfWork;
	private readonly ApplicationDbContext _context = context;

	

	public async Task<Result<StudentResponse>> GetAsync(int DepartmentId, Guid Id, CancellationToken cancellationToken = default)
	{
		var departmentIsExist = await _unitOfWork.Repository<Department>().AnyAsync(x => x.Id == DepartmentId, cancellationToken);

		if (!departmentIsExist)
			return Result.Failure<StudentResponse>(DepartmentErrors.DepartmentNotFound);

		var Student = await _unitOfWork.Repository<Student>().GetAsQueryable()
			.Where(x => x.Id == Id && x.DepartmentId == DepartmentId)
			.Include(x => x.StudentsSubjects)
			.ThenInclude(x => x.Subject)
			.Select(x => new StudentResponse
			(
				x.FirstName,
				 x.LastName,
				 x.Address,
				 x.Phone,
				 x.StudentsSubjects.Select(y => new StudentSubjectResponse(y.Subject.Name)).ToList()
			)).SingleOrDefaultAsync(cancellationToken);

		if (Student is null)
			return Result.Failure<StudentResponse>(StudentErrors.StudentNotFound);

		return Result.Success(Student);
	}

public async Task<Result<IEnumerable<StudentBasicResponse>>> GetAllAsync(int DepartmentId, CancellationToken cancellationToken = default)
	{
		var departmentIsExist = await _unitOfWork.Repository<Department>().AnyAsync(x => x.Id == DepartmentId, cancellationToken);

		if (!departmentIsExist)
			return Result.Failure<IEnumerable<StudentBasicResponse>>(DepartmentErrors.DepartmentNotFound);

		var Students = await _unitOfWork.Repository<Student>().FindAllProjectedAsync<StudentBasicResponse>(
			x => x.DepartmentId == DepartmentId,
			cancellationToken: cancellationToken
		);

		return Result.Success(Students);
	}

	public async Task<Result<StudentBasicResponse>> AddAsync(int DepartmentId, StudentRequest request, CancellationToken cancellationToken = default)
	{
		var departmentIsExist = await _unitOfWork.Repository<Department>().AnyAsync(x => x.Id == DepartmentId, cancellationToken);

		if (!departmentIsExist)
			return Result.Failure<StudentBasicResponse>(DepartmentErrors.DepartmentNotFound);

		var student = request.Adapt<Student>();
		student.DepartmentId = DepartmentId;

	   await _unitOfWork.Repository<Student>().CreateAsync(student, cancellationToken);
		await _unitOfWork.CompleteAsync(cancellationToken);

		return Result.Success(student.Adapt<StudentBasicResponse>());

	}

	public async Task<Result> UpdateAsync(int DepartmentId, Guid id, StudentRequest request, CancellationToken cancellationToken = default)
	{
		var departmentIsExist = await _unitOfWork.Repository<Department>().AnyAsync(x => x.Id == DepartmentId, cancellationToken);

		if (!departmentIsExist)
			return Result.Failure(DepartmentErrors.DepartmentNotFound);

		var currentStudent = await _unitOfWork.Repository<Student>().FindAsync(x=>x.Id==id && x.DepartmentId==DepartmentId,null,cancellationToken);

		if (currentStudent is null)
			return Result.Failure(StudentErrors.StudentNotFound);

		var studentIsExist = await _unitOfWork.Repository<Student>().AnyAsync(x=>x.FirstName==request.FirstName
	    && x.LastName==request.LastName && x.Phone==request.Phone && x.Address == request.Address && x.Id != id,cancellationToken);

		if(studentIsExist)
			return Result.Failure(StudentErrors.DuplicatedStudent);

		currentStudent = request.Adapt(currentStudent);



		_unitOfWork.Repository<Student>().Update(currentStudent);
		await _unitOfWork.CompleteAsync(cancellationToken);
		return Result.Success();

	}

	public async Task<Result> ToggleStatusAsync(int DepartmentId, Guid id, CancellationToken cancellationToken = default)
	{
		var departmentIsExist = await _unitOfWork.Repository<Department>().AnyAsync(x => x.Id == DepartmentId, cancellationToken);

		if (!departmentIsExist)
			return Result.Failure(DepartmentErrors.DepartmentNotFound);

		var currentStudent = await _unitOfWork.Repository<Student>().FindAsync(x => x.Id == id && x.DepartmentId == DepartmentId, null, cancellationToken);

		if (currentStudent is null)
			return Result.Failure(StudentErrors.StudentNotFound);

		currentStudent.IsActive = !currentStudent.IsActive;

		await _unitOfWork.CompleteAsync(cancellationToken);
		return Result.Success();
	}

	
}
