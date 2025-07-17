using SchoolProject.Application.Abstractions;
using SchoolProject.Application.Contracts.Teacher;
using SchoolProject.Application.ErrorHandler;
using SchoolProject.Application.Interfaces.IServices;
using SchoolProject.Application.Interfaces.IUnitOfWork;
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
			.Where(x => x.Id == teacherId)
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
}
