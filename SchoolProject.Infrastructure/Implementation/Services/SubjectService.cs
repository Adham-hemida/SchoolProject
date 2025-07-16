using Mapster;
using SchoolProject.Application.Abstractions;
using SchoolProject.Application.Contracts.Subject;
using SchoolProject.Application.ErrorHandler;
using SchoolProject.Application.Interfaces.IServices;
using SchoolProject.Application.Interfaces.IUnitOfWork;
using SchoolProject.Domain.Entites;
using SchoolProject.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Infrastructure.Implementation.Services;
public class SubjectService(IUnitOfWork unitOfWork, ApplicationDbContext context) : ISubjectService
{
	private readonly IUnitOfWork _unitOfWork = unitOfWork;
	private readonly ApplicationDbContext _context = context;



	public async Task<Result<SubjectResponse>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
	{
		var subject = await _unitOfWork.Repository<Subject>().GetByIdAsync(id, cancellationToken);

		if (subject is null)
			return Result.Failure<SubjectResponse>(SubjectErrors.SubjectNotFound);

		return Result.Success(subject.Adapt<SubjectResponse>());
	}

	public async Task<Result<IEnumerable<SubjectResponse>>> GetAllAsync(CancellationToken cancellationToken = default)
	{
		var subjects = await _unitOfWork.Repository<Subject>().GetAsQueryable()
			.Where(x => x.IsActive)
			.Include(x => x.Teacher)
			.ProjectToType<SubjectResponse>()
			.ToListAsync(cancellationToken);

		return Result.Success(subjects.AsEnumerable());
	}

	public async Task<Result<SubjectResponse>> AddAsync(SubjectRequest request, CancellationToken cancellationToken = default)
	{
		var  subjectIsExist=await _unitOfWork.Repository<Subject>()
			.AnyAsync(x => x.Name.ToLower() == request.Name.ToLower().Trim(), cancellationToken: cancellationToken);

		if(subjectIsExist)
			return Result.Failure<SubjectResponse>(SubjectErrors.DuplicatedSubject);

		var teacher=await _unitOfWork.Repository<Teacher>()
			.GetAsQueryable().FirstAsync(x => x.Id == request.TeacherId , cancellationToken);

		if (teacher is null)
			return Result.Failure<SubjectResponse>(TeacherErrors.TeacherNotFound);

		var subject = request.Adapt<Subject>();
		await _unitOfWork.Repository<Subject>().CreateAsync(subject);
		await _unitOfWork.CompleteAsync(cancellationToken);
		return Result.Success(subject.Adapt<SubjectResponse>());
	}

	public async Task<Result> UpdateAsync(int id, SubjectRequest request, CancellationToken cancellationToken = default)
	{
		var subject=await _unitOfWork.Repository<Subject>().GetByIdAsync(id, cancellationToken);

		if (subject is null)
			return Result.Failure(SubjectErrors.SubjectNotFound);

		var teacher = await _unitOfWork.Repository<Teacher>()
			.GetAsQueryable().FirstAsync(x => x.Id == request.TeacherId, cancellationToken);

		if (teacher is null)
			return Result.Failure<SubjectResponse>(TeacherErrors.TeacherNotFound);

		var subjectIsExist= await _unitOfWork.Repository<Subject>().AnyAsync(x=>x.Name==request.Name && x.CreditHours==request.CreditHours && x.Id!=id, cancellationToken);

		if (subjectIsExist)
			return Result.Failure(SubjectErrors.DuplicatedSubject);

		subject = request.Adapt(subject);
		 _unitOfWork.Repository<Subject>().Update(subject);
		await _unitOfWork.CompleteAsync(cancellationToken);
		return Result.Success();
	}

	public async Task<Result> ToggleStatusAsync(int id, CancellationToken cancellationToken = default)
	{
		var subject = await _unitOfWork.Repository<Subject>().GetByIdAsync(id, cancellationToken);

		if (subject is null)
			return Result.Failure(StudentErrors.StudentNotFound);

		subject.IsActive = !subject.IsActive;

		await _unitOfWork.CompleteAsync(cancellationToken);
		return Result.Success();
	}

	public async Task<Result> ToggleStatusForStudentSubjectAsync(int id, Guid studentId, int departmentId, CancellationToken cancellationToken = default)
	{
		var departmentIsExist = await _unitOfWork.Repository<Department>().AnyAsync(x => x.Id == departmentId, cancellationToken);

		if (!departmentIsExist)
			return Result.Failure(DepartmentErrors.DepartmentNotFound);

		var student = await _unitOfWork.Repository<Student>()
		.GetAsQueryable()
		.Include(s => s.StudentsSubjects)
		.FirstOrDefaultAsync(s => s.Id == studentId && s.DepartmentId == departmentId, cancellationToken);
		
		if (student is null)
			return Result.Failure(StudentErrors.StudentNotFound);

		var subject = await _unitOfWork.Repository<Subject>().GetByIdAsync(id, cancellationToken);

		if (subject is null)
			return Result.Failure(StudentErrors.StudentNotFound);

		var studentSubject = student.StudentsSubjects.FirstOrDefault(ss => ss.SubjectId == id);
		if (studentSubject == null)
			return Result.Failure(SubjectErrors.SubjectNotFound);

		studentSubject.IsActive=!studentSubject.IsActive;
		_unitOfWork.Repository<StudentSubject>().Update(studentSubject);
		await _unitOfWork.CompleteAsync(cancellationToken);
		return Result.Success();






	}
}
