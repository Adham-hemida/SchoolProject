using SchoolProject.Application.Abstractions;
using SchoolProject.Application.Contracts.Department;
using SchoolProject.Application.Contracts.DepartmentSubject;
using SchoolProject.Application.Contracts.Subject;
using SchoolProject.Application.ErrorHandler;
using SchoolProject.Application.Interfaces.IServices;
using SchoolProject.Application.Interfaces.IUnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Infrastructure.Implementation.Services;
public class DepartmentSubjectService(IUnitOfWork unitOfWork) : IDepartmentSubjectService
{
	private readonly IUnitOfWork _unitOfWork = unitOfWork;

	public async Task<Result<DepartmentSubjectResponse>> GetAllSubjectsOfDepartmentAsync(int departmentId, CancellationToken cancellationToken = default)
	{
		var departmentIsExist=await _unitOfWork.Repository<Department>().AnyAsync(x=>x.Id==departmentId, cancellationToken);

		if (!departmentIsExist)
			return Result.Failure<DepartmentSubjectResponse>(DepartmentErrors.DepartmentNotFound);

		var departmentResponse=await _unitOfWork.Repository<Department>().GetAsQueryable()
			.Where(x=>x.Id == departmentId)
			.Include(x => x.DepartmentSubjects)
			.ThenInclude(x => x.Subject)
			.Select(x=> new DepartmentSubjectResponse
			(
				x.Id,
				x.Name,
				x.DepartmentSubjects.Select(y => new SubjectDetailsResponse(y.Subject.Id,y.Subject.Name,y.Subject.IsActive)).ToList()
			)).SingleOrDefaultAsync(cancellationToken);

		if (departmentResponse is null)
			return Result.Failure<DepartmentSubjectResponse>(DepartmentErrors.DepartmentNotFound);

		return Result.Success(departmentResponse);

	}

}
