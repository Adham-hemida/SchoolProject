using SchoolProject.Application.Abstractions;
using SchoolProject.Application.Contracts.StudentSubject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.Interfaces.IServices;
public interface IStudentSubjectService
{
	Task<Result<StudentSubjectResponse>> AddGradeToStudentAsync(int subjectId, Guid studentId,StudentSubjectRequest request, CancellationToken cancellationToken = default);
	Task<Result<IEnumerable<StudentSubjectResponse>>> GetStudentWithAllSubjectsGradeAsync(Guid studentId, CancellationToken cancellationToken = default);
}
