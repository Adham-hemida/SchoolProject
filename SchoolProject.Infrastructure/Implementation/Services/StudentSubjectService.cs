using Microsoft.EntityFrameworkCore;
using SchoolProject.Application.Abstractions;
using SchoolProject.Application.Consts;
using SchoolProject.Application.Contracts.StudentSubject;
using SchoolProject.Application.ErrorHandler;
using SchoolProject.Application.Interfaces.IServices;
using SchoolProject.Application.Interfaces.IUnitOfWork;
using SchoolProject.Domain.Entites;
using SchoolProject.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Infrastructure.Implementation.Services;
public class StudentSubjectService(IUnitOfWork unitOfWork) : IStudentSubjectService
{
	private readonly IUnitOfWork _unitOfWork = unitOfWork;

	public async Task<Result<StudentSubjectResponse>> AddGradeToStudentAsync(int subjectId, Guid studentId, StudentSubjectRequest request, CancellationToken cancellationToken = default)
	{
		var subject = await _unitOfWork.Repository<Subject>()
			.GetByIdAsync(subjectId, cancellationToken);

		if (subject is null)
			return Result.Failure<StudentSubjectResponse>(SubjectErrors.SubjectNotFound);

		var student = await _unitOfWork.Repository<Student>().GetAsQueryable()
				.Where(x => x.Id == studentId)
				.Include(x => x.StudentsSubjects)
				.ThenInclude(x => x.Subject)
				.SingleOrDefaultAsync(cancellationToken);

		if (student is null)
			return Result.Failure<StudentSubjectResponse>(StudentErrors.StudentNotFound);

		var studentSubject = student.StudentsSubjects
			.FirstOrDefault(x => x.SubjectId == subjectId && x.IsActive);

		if (studentSubject is null)
			return Result.Failure<StudentSubjectResponse>(StudentSubjectErrors.StudentSubjectNotFound);

		studentSubject.Grade = request.Grade;
		studentSubject.IsPassed = request.IsPassed;

		var studentSubjectResponse = new StudentSubjectResponse
		(
			studentSubject.Subject.Name,
			studentSubject.Grade,
			studentSubject.IsPassed
		);



		_unitOfWork.Repository<StudentSubject>().Update(studentSubject);
		await _unitOfWork.CompleteAsync(cancellationToken);
		return Result.Success(studentSubjectResponse);

	}

	public async Task<Result<IEnumerable<StudentSubjectResponse>>> GetStudentWithAllSubjectsGradeAsync(Guid studentId, CancellationToken cancellationToken = default)
	{
		var studentSubjects = await _unitOfWork.Repository<Student>().GetAsQueryable()
			.Where(x => x.Id == studentId)
			.Include(x => x.StudentsSubjects)
			.ThenInclude(x => x.Subject)
			.Select(x => x.StudentsSubjects.Select(y => new StudentSubjectResponse
				(
					y.Subject.Name,
					y.Grade,
					y.IsPassed
				)).ToList())
			.SingleOrDefaultAsync(cancellationToken);

		if(studentSubjects is null)
		return Result.Failure<IEnumerable<StudentSubjectResponse>>(StudentSubjectErrors.StudentSubjectNotFound);


		return Result.Success<IEnumerable<StudentSubjectResponse>>(studentSubjects);
	}
}
