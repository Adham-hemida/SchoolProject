using Mapster;
using SchoolProject.Application.Abstractions;
using SchoolProject.Application.Contracts.Teacher;
using SchoolProject.Application.ErrorHandler;
using SchoolProject.Application.Interfaces.IServices;
using SchoolProject.Application.Interfaces.IUnitOfWork;
using SchoolProject.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Infrastructure.Implementation.Services;
public class TeacherService(IUnitOfWork unitOfWork) : ITeacherService
{
	private readonly IUnitOfWork _unitOfWork = unitOfWork;

	public async Task<Result<TeacherResponse>> GetByIdAsync(Guid teacherId, CancellationToken cancellationToken = default)
	{
		var teacher = await _unitOfWork.Repository<Teacher>().GetAsQueryable()
			.Where(x => x.Id == teacherId && x.IsActive)
			.Select(x => new TeacherResponse(
				x.Id,
				x.FirstName,
				x.LastName,
				x.Email,
				x.Phone,
				x.IsActive,
				x.Subjects.Select(s => s.Name).Distinct()
			)).SingleOrDefaultAsync(cancellationToken);

		if (teacher is null)
			return Result.Failure<TeacherResponse>(TeacherErrors.TeacherNotFound);

		return Result.Success(teacher);
	}

	public async Task<Result<IEnumerable<TeacherResponse>>> GetAllAsync(CancellationToken cancellationToken = default)
	{
		var teachers = await _unitOfWork.Repository<Teacher>().GetAsQueryable()
			.Select(x => new TeacherResponse(
				x.Id,
				x.FirstName,
				x.LastName,
				x.Email,
				x.Phone,
				x.IsActive,
				x.Subjects.Select(s => s.Name).Distinct()
			)).ToListAsync(cancellationToken);



		return Result.Success<IEnumerable<TeacherResponse>>(teachers);
	}


	public async Task<Result<TeacherBaiscResponse>> AddAsync(TeacherRequest request, CancellationToken cancellationToken = default)
	{
		var teacherIsExist = await _unitOfWork.Repository<Teacher>().GetAsQueryable()
			.AnyAsync(x => x.Email == request.Email, cancellationToken);

		if (teacherIsExist)
			return Result.Failure<TeacherBaiscResponse>(TeacherErrors.DuplicatedTeacher);

		var teacher = request.Adapt<Teacher>();
		await _unitOfWork.Repository<Teacher>().CreateAsync(teacher, cancellationToken);

		await _unitOfWork.CompleteAsync(cancellationToken);
		return Result.Success(teacher.Adapt<TeacherBaiscResponse>());

	}

	public async Task<Result<TeacherBaiscResponse>> UpdateAsync(Guid teacherId, TeacherRequest request, CancellationToken cancellationToken = default)
	{
		var teacherIsExist = await _unitOfWork.Repository<Teacher>().GetAsQueryable()
			.AnyAsync(x => x.Email == request.Email && x.Id != teacherId, cancellationToken);

		if (teacherIsExist)
			return Result.Failure<TeacherBaiscResponse>(TeacherErrors.DuplicatedTeacher);

		var teacher = await _unitOfWork.Repository<Teacher>()
				.FindAsync(x => x.Id == teacherId, null, cancellationToken);

		if (teacher is null)
			return Result.Failure<TeacherBaiscResponse>(TeacherErrors.TeacherNotFound);

		request.Adapt(teacher);

		_unitOfWork.Repository<Teacher>().Update(teacher);

		await _unitOfWork.CompleteAsync(cancellationToken);
		return Result.Success(teacher.Adapt<TeacherBaiscResponse>());

	}

	public async Task<Result> ToggleStatusAsync(Guid id, CancellationToken cancellationToken = default)
	{
		var teacher = await _unitOfWork.Repository<Teacher>()
				.FindAsync(x => x.Id == id, null, cancellationToken);

		if (teacher is null)
			return Result.Failure(StudentErrors.StudentNotFound);

		teacher.IsActive = !teacher.IsActive;

		_unitOfWork.Repository<Teacher>().Update(teacher);
		await _unitOfWork.CompleteAsync(cancellationToken);
		return Result.Success();
	}
}
