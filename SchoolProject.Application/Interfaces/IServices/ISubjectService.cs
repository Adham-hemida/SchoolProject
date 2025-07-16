using SchoolProject.Application.Abstractions;
using SchoolProject.Application.Contracts.Department;
using SchoolProject.Application.Contracts.Subject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.Interfaces.IServices;
public interface ISubjectService
{
	Task<Result<SubjectResponse>> GetByIdAsync(int id, CancellationToken cancellationToken = default!);
	Task<Result<IEnumerable<SubjectResponse>>> GetAllAsync(CancellationToken cancellationToken = default);
	Task<Result<SubjectResponse>> AddAsync(SubjectRequest request, CancellationToken cancellationToken = default);
	Task<Result> UpdateAsync(int id, SubjectRequest request, CancellationToken cancellationToken = default);
	Task<Result> ToggleStatusAsync(int id, CancellationToken cancellationToken = default);
	Task<Result> ToggleStatusForStudentSubjectAsync(int id,Guid studentId,int departmentId, CancellationToken cancellationToken = default);
	Task<Result> AddSubjectToDepartmentAsync(int id, int departmentId, bool isMandatory, CancellationToken cancellationToken = default);

}
