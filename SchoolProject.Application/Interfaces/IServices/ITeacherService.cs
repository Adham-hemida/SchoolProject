using SchoolProject.Application.Abstractions;
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
	Task<Result<IEnumerable<TeacherResponse>>> GetAllAsync(CancellationToken cancellationToken = default);
	Task<Result<TeacherBaiscResponse>> AddAsync(TeacherRequest request, CancellationToken cancellationToken = default);
	Task<Result<TeacherBaiscResponse>> UpdateAsync(Guid teacherId, TeacherRequest request, CancellationToken cancellationToken = default);
}
