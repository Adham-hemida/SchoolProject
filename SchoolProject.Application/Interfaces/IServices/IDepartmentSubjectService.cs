using SchoolProject.Application.Abstractions;
using SchoolProject.Application.Contracts.DepartmentSubject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.Interfaces.IServices;
public interface IDepartmentSubjectService
{
	Task<Result<DepartmentSubjectResponse>>GetAllSubjectsOfDepartmentAsync(int departmentId, CancellationToken cancellationToken = default!);
}
