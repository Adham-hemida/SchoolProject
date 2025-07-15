using SchoolProject.Application.Abstractions;
using SchoolProject.Application.Contracts.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.Interfaces.IServices;
public interface IDepartmentService
{
	Task<Result<DepartmentResponse>> GetByIdAsync(int id, CancellationToken cancellationToken = default!);
	
}
