using SchoolProject.Application.Abstractions;
using SchoolProject.Application.Contracts.Common;
using SchoolProject.Application.Contracts.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.Interfaces.IServices;
public interface ITeacherService
{
	Task<Result<TeacherResponse>> GetByIdAsync(Guid teacherId, CancellationToken cancellationToken = default!);
	Task<Result<PaginatedList<TeacherResponse>>> GetAllAsync(RequestFilters filters, CancellationToken cancellationToken = default); Task<Result<TeacherBaiscResponse>> AddAsync(TeacherRequest request, CancellationToken cancellationToken = default);
	Task<Result<TeacherBaiscResponse>> UpdateAsync(Guid teacherId, TeacherRequest request, CancellationToken cancellationToken = default);
	Task<Result> ToggleStatusAsync(Guid id, CancellationToken cancellationToken = default);
	Task<Result> AddSubjectToTeacherAsync(int subjectId, Guid teacherId, CancellationToken cancellationToken = default);
}
