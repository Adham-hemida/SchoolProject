using SchoolProject.Application.Abstractions;
using SchoolProject.Application.Contracts.Department;
using SchoolProject.Application.Contracts.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.Interfaces.IServices;
public interface IDepartmentService
{
	Task<Result<DepartmentResponse>> GetByIdAsync(int id, CancellationToken cancellationToken = default!);
	Task<Result<IEnumerable<DepartmentResponse>>> GetAllAsync( CancellationToken cancellationToken = default);
	Task<Result<DepartmentResponse>> AddAsync( DepartmentRequest request, CancellationToken cancellationToken = default);
	Task<Result> UpdateAsync( int id, DepartmentRequest request, CancellationToken cancellationToken = default);


}
