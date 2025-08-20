using Mapster;
using SchoolProject.Application.Abstractions;
using SchoolProject.Application.Contracts.Common;
using SchoolProject.Application.Contracts.Student;
using SchoolProject.Application.ErrorHandler;
using SchoolProject.Application.Interfaces.IServices;
using SchoolProject.Application.Interfaces.IUnitOfWork;
using SchoolProject.Infrastructure.Data;
using System.Linq.Dynamic.Core;

namespace SchoolProject.Infrastructure.Implementation.Services;
public class StudentService(IUnitOfWork unitOfWork) : IStudentService
{
	private readonly IUnitOfWork _unitOfWork = unitOfWork;



	public async Task<Result<StudentResponse>> GetAsync(int departmentId, Guid Id, CancellationToken cancellationToken = default)
	{
		var departmentIsExist = await _unitOfWork.Repository<Department>().AnyAsync(x => x.Id == departmentId, cancellationToken);

		if (!departmentIsExist)
			return Result.Failure<StudentResponse>(DepartmentErrors.DepartmentNotFound);

		var student = await _unitOfWork.Repository<Student>().GetAsQueryable()
			.Where(x => x.Id == Id && x.DepartmentId == departmentId)
			.Include(x => x.StudentsSubjects)
			.ThenInclude(x => x.Subject)
			.Select(x => new StudentResponse
			(
				x.FirstName,
				 x.LastName,
				 x.Address,
				 x.Phone,
				 x.StudentsSubjects.Where(ss =>ss.IsActive && ss.Subject.IsActive ).Select(y => new StudentSubjectResponse(y.Subject.Name )).Distinct().ToList()
			)).SingleOrDefaultAsync(cancellationToken);

		if (student is null)
			return Result.Failure<StudentResponse>(StudentErrors.StudentNotFound);

		return Result.Success(student);
	}

	public async Task<Result<PaginatedList<StudentBasicResponse>>> GetAllAsync(int departmentId, RequestFilters filters, CancellationToken cancellationToken = default)
	{
		var departmentIsExist = await _unitOfWork.Repository<Department>().AnyAsync(x => x.Id == departmentId, cancellationToken);

		if (!departmentIsExist)
			return Result.Failure<PaginatedList<StudentBasicResponse>>(DepartmentErrors.DepartmentNotFound);

		var query = _unitOfWork.Repository<Student>().GetAsQueryable()
			.Where(x => x.DepartmentId == departmentId);
		if (!string.IsNullOrEmpty(filters.SearchValue))
		{
			query = query.Where(x =>x.FirstName.Contains(filters.SearchValue) || x.LastName.Contains(filters.SearchValue));
		}
		if (!string.IsNullOrEmpty(filters.SortColumn))
		{
			query = query.OrderBy($"{filters.SortColumn} {filters.SortDirection}");
		}

		var source = query.Select(x => new StudentBasicResponse
		(
			x.Id,
			x.FirstName,
			x.LastName,
			x.Phone
		)).AsNoTracking();

		var students = await PaginatedList<StudentBasicResponse>.CreateAsync(source, filters.PageNumber, filters.PageSize, cancellationToken);

		return Result.Success(students);
	}

	public async Task<Result<StudentBasicResponse>> AddAsync(int departmentId, StudentRequest request, CancellationToken cancellationToken = default)
	{
		var departmentIsExist = await _unitOfWork.Repository<Department>().AnyAsync(x => x.Id == departmentId, cancellationToken);

		if (!departmentIsExist)
			return Result.Failure<StudentBasicResponse>(DepartmentErrors.DepartmentNotFound);

		var studentIsExists = await _unitOfWork.Repository<Student>().AnyAsync(x =>
		x.FirstName.Trim().ToLower() == request.FirstName.Trim().ToLower()
		&& x.LastName.Trim().ToLower() == request.LastName.Trim().ToLower()
		&& x.Phone == request.Phone
		&& x.DepartmentId == departmentId,
		cancellationToken);

		if (studentIsExists)
			return Result.Failure<StudentBasicResponse>(StudentErrors.DuplicatedStudent);

		var student = request.Adapt<Student>();
		student.DepartmentId = departmentId;

		var departmentSubjectIds = await _unitOfWork.Repository<DepartmentSubject>().GetAsQueryable()
			.Where(x => x.DepartmentId == departmentId && x.Subject.IsActive)
			.Select(x => x.SubjectId)
			.ToListAsync(cancellationToken);

		var validSubjectIds = request.SubjectIds
			.Where(id => departmentSubjectIds.Contains(id))
			.Distinct()
			.ToList();

		if (validSubjectIds.Count == 0)
			return Result.Failure<StudentBasicResponse>(SubjectErrors.NoValidSubjects);

		foreach (var subjectId in validSubjectIds)
		{
			var alreadyExists = student.StudentsSubjects.Any(ss => ss.SubjectId == subjectId);

			if (!alreadyExists)
			{
				student.StudentsSubjects.Add(new StudentSubject
				{
					SubjectId = subjectId,
					IsPassed = false
				});
			}
		}



		await _unitOfWork.Repository<Student>().CreateAsync(student, cancellationToken);
		await _unitOfWork.CompleteAsync(cancellationToken);

		return Result.Success(student.Adapt<StudentBasicResponse>());

	}

	public async Task<Result> UpdateAsync(int departmentId, Guid id, UpdateStudentRequest request, CancellationToken cancellationToken = default)
	{
		var departmentIsExist = await _unitOfWork.Repository<Department>().AnyAsync(x => x.Id == departmentId, cancellationToken);

		if (!departmentIsExist)
			return Result.Failure(DepartmentErrors.DepartmentNotFound);

		var currentStudent = await _unitOfWork.Repository<Student>().FindAsync(x => x.Id == id && x.DepartmentId == departmentId, null, cancellationToken);

		if (currentStudent is null)
			return Result.Failure(StudentErrors.StudentNotFound);

		var studentIsExist = await _unitOfWork.Repository<Student>().AnyAsync(x => x.FirstName == request.FirstName
		&& x.LastName == request.LastName && x.Phone == request.Phone && x.Address == request.Address && x.Id != id, cancellationToken);

		if (studentIsExist)
			return Result.Failure(StudentErrors.DuplicatedStudent);

		currentStudent = request.Adapt(currentStudent);

		_unitOfWork.Repository<Student>().Update(currentStudent);
		await _unitOfWork.CompleteAsync(cancellationToken);
		return Result.Success();
	}



	public async Task<Result> AssignStudentToDepartmentAsync(int departmentId, Guid id, CancellationToken cancellationToken = default)
	{
		var departmentIsExist = await _unitOfWork.Repository<Department>().AnyAsync(x => x.Id == departmentId, cancellationToken);

		if (!departmentIsExist)
			return Result.Failure(DepartmentErrors.DepartmentNotFound);

		var student=await _unitOfWork.Repository<Student>().FindAsync(x=>x.Id==id,null,cancellationToken);
		if(student is null)
			return Result.Failure(StudentErrors.StudentNotFound);


		var studentIsExistInDepartment = await _unitOfWork.Repository<Student>().AnyAsync(x => x.Id == id && x.DepartmentId == departmentId, cancellationToken);

		if(studentIsExistInDepartment)
			return Result.Failure(StudentErrors.DuplicatedStudent);

		student.DepartmentId= departmentId;

		 _unitOfWork.Repository<Student>().Update(student);
		await _unitOfWork.CompleteAsync(cancellationToken);
		return Result.Success();

		

	}


	public async Task<Result> ToggleStatusAsync(int departmentId, Guid id, CancellationToken cancellationToken = default)
	{
		var departmentIsExist = await _unitOfWork.Repository<Department>().AnyAsync(x => x.Id == departmentId, cancellationToken);

		if (!departmentIsExist)
			return Result.Failure(DepartmentErrors.DepartmentNotFound);

		var currentStudent = await _unitOfWork.Repository<Student>().FindAsync(x => x.Id == id && x.DepartmentId == departmentId, null, cancellationToken);

		if (currentStudent is null)
			return Result.Failure(StudentErrors.StudentNotFound);

		currentStudent.IsActive = !currentStudent.IsActive;

		_unitOfWork.Repository<Student>().Update(currentStudent);
		await _unitOfWork.CompleteAsync(cancellationToken);
		return Result.Success();
	}


}
